using Ayamaki.Core.GameAPI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Ayamaki.Core.GameManager
{
    public class SceneController : MonoBehaviour
    {

        public UnityEvent OnSceneBegin;
        public UnityEvent OnSceneLeave;
        //public UnityEvent onSceneWaitingLoad;

        void Start()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            if (!activeScene.isLoaded)
                return;

            Debug.Log("wtf");
            OnSceneBegin.Invoke();
            LuaCore.Instance.Eval("print(tostring(Game.getLocal(\"keyCollected\")))");
        }

        void Update()
        {

        }
        
        public void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
            OnSceneLeave.Invoke();
        }
    }
}
