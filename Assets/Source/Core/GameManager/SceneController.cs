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

        void Awake()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            if (!activeScene.isLoaded)
                return;

            OnSceneBegin.Invoke();
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
