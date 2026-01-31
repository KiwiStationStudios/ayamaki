using System;
using UnityEngine;
using Ayamaki.Core.GameManager;
using Ayamaki.Core.GameAPI;
using Lua;

namespace Ayamaki.Core.GameAPI
{   
    [DefaultExecutionOrder(-999)]
    public class LuaAPI
    {
        /*public static void StartAPI(Table gameAPI)
        {
            gameAPI["getLocal"] = (Func<string, object>)(key => (object)GameManager.GameManager.Instance.GetLocal<object>(key));

            gameAPI["setLocal"] = (Func<string, object, object>)((key, value) => {
                GameManager.GameManager.Instance.SetLocal<object>(key, value);
                return null;
            });
        }*/

        public static void StartAPI()
        {
            Debug.Log("setup complete");

            LuaTable GameAPI = LuaCore.Instance.RegisterTable("Game");

            GameAPI["getLocal"] = new LuaFunction((context, ct) => {
                var arg_Key = context.GetArgument<string>(0);
                // return as lua table //
                var value = GameManager.GameManager.Instance.GetLocal<string>(arg_Key);
                Debug.Log(value);
                var t_dataContext = (LuaValue)GameManager.GameManager.Instance.GetLocal<string>(arg_Key);

                return new(context.Return(t_dataContext));
            });

            GameAPI["setLocal"] = new LuaFunction((context, ct) => {
                var arg_Key = context.GetArgument<string>(0);
                var arg_Value = context.GetArgument(1);
                
                GameManager.GameManager.Instance.SetLocal<object>(arg_Key, arg_Value);
                return new(0);
            });

            /*GameAPI["getObjectByName"] = new LuaFunction((ctx, ct) => {
                var arg_objectName = ctx.GetArgument<string>(0);
                var obj = GameObject.Find(arg_objectName);
                var ud = new Lua.User
                return new(obj != null ? new (ctx.Return(obj)) : new(0));
            });*/

            GameAPI["getByName"] = new LuaFunction((ctx, ct) => {
                var name = ctx.GetArgument<string>(0);
                var go = GameObject.Find(name);
                if (go == null)
                    return new(0);

                // aqui converte para LuaObject
                var luaGo = new LuaGameObject(go);
                return new(ctx.Return(luaGo));
            });
        }
    }
}
