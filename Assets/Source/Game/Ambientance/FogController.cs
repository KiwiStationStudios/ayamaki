using UnityEngine;

namespace Ayamaki.Game.Ambientance
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class FogController : MonoBehaviour
    {
        public GameObject fogObject;
        public Color fogColor = new(0f, 0f, 0f, 1f);
        public float fogRadius = 4f;
        public float fogDensity = 11f;
        private Renderer rd;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rd = fogObject.GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            rd.sharedMaterial.SetColor("_BaseColor", fogColor);
            rd.sharedMaterial.SetColor("_EmissionColor", fogColor);
            rd.sharedMaterial.SetFloat("_SoftParticlesFarFadeDistance", fogDensity);

            fogObject.transform.localScale = new Vector3(fogRadius, fogRadius, fogRadius);
        }
    }
}
