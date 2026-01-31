using UnityEngine;
using Lua;
using System;

namespace Ayamaki.Core.GameAPI
{
    public class LuaCallbackBus : MonoBehaviour
    {
        public static LuaCallbackBus Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }

            Destroy(this);
        }

        /// <summary>
        /// Emite um evento, procurando e executando a função correspondente em Game
        /// </summary>
        /// <param name="eventName">Nome do callback (ex: "onInteract")</param>
        /// <param name="args">Argumentos variáveis a passar para a função Lua</param>
        public async void Emit(string eventName, params object[] args)
        {
            try
            {
                // Procura a função em Game[eventName]
                var gameTableValue = LuaCore.state.Environment["Game"];
                var gameTable = gameTableValue.Read<LuaTable>();
                var callback = gameTable[eventName];

                // Se encontrou e é uma função válida, chama
                if (callback.Type == LuaValueType.Function)
                {
                    // Converte os argumentos para LuaValue
                    var luaArgs = new LuaValue[args.Length];
                    for (int i = 0; i < args.Length; i++)
                    {
                        luaArgs[i] = (LuaValue)args[i];
                    }
                    
                    await LuaCore.state.CallAsync((LuaValue)callback, luaArgs);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Lua callback '{eventName}' error: {e}");
            }
        }
    }
}