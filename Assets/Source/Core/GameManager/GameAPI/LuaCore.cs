using System;
using UnityEngine;
using Lua;
using Lua.Standard;
using Lua.Platforms;
using Lua.IO;
using Lua.Unity;

namespace Ayamaki.Core.GameAPI
{
    [DefaultExecutionOrder(-3000)]
    public class LuaCore : MonoBehaviour
    {
        public static LuaCore Instance;
        public static LuaState state;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                LuaPlatform platform = new(
                    FileSystem: new FileSystem(),
                    OsEnvironment: new UnityApplicationOsEnvironment(),
                    StandardIO: new UnityStandardIO(),
                    TimeProvider: TimeProvider.System
                );

                state = LuaState.Create(platform);
                state.OpenStandardLibraries();
                Debug.Log("LuaCore Setup complete");
                DontDestroyOnLoad(this);
                return;
            }

            Destroy(this);
        }

        public async void Eval(string fn)
        {
            await state.DoStringAsync(fn);
        }

        public void RegisterFunction(string name, LuaFunction luaFunc)
        {
            state.Environment[name] = luaFunc;
        }
        
        public LuaTable RegisterTable(string name)
        {
            var luaTable = new LuaTable();
            state.Environment[name] = luaTable;
            return luaTable;
        }
    }
}
