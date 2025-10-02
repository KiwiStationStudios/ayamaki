using UnityEngine;

namespace Ayamaki.Core.View
{
    [ExecuteInEditMode]
    public class BillboardView : MonoBehaviour
    {
        private Transform cam;
        void Awake()
        {
            if (cam == null)
                cam = GameObject.FindWithTag("MainCamera").transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (cam == null) return;

            transform.LookAt(cam);
            transform.rotation = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0);
        }
    }
}
