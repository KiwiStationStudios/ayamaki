using UnityEngine;
using Lua;
using System;

namespace Ayamaki.Core.GameAPI
{
    [LuaObject]
    public partial class LuaGameObject
    {
        public GameObject m_go;
        public LuaGameObject(GameObject go)
        {
            m_go = go;
        }

        public string Name
        {
            get => m_go.name;
            set => m_go.name = value;
        }

        [LuaMember("destroy")]
        public void Destroy()
        {
            UnityEngine.Object.Destroy(m_go);
        }
    }
}