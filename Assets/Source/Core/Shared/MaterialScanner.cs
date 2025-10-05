using UnityEngine;

namespace Ayamaki.Core.Shared
{
    public class MaterialScanner : MonoBehaviour
    {
        [Header("Configurações")]
        public Transform rayOrigin; // normalmente o player
        public float rayDistance = 1.5f;
        public LayerMask groundMask;

        [Header("Debug")]
        public bool debugRay = true;

        [Header("Resultado")]
        public Material currentMaterial;
        public string currentMaterialName = "";

        void Update()
        {
            ScanMaterial();
        }

        void ScanMaterial()
        {
            if (Physics.Raycast(rayOrigin.position, Vector3.down, out RaycastHit hit, rayDistance, groundMask))
            {
                currentMaterial = GetMaterialFromHit(hit);

                if (debugRay)
                    Debug.DrawLine(rayOrigin.position, hit.point, Color.green);

                if (currentMaterial != null)
                    currentMaterialName = currentMaterial.name;
            }
            else
            {
                if (debugRay)
                    Debug.DrawRay(rayOrigin.position, Vector3.down * rayDistance, Color.red);

                currentMaterial = null;
                currentMaterialName = "";
            }
        }

        Material GetMaterialFromHit(RaycastHit hit)
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
            {
                // Se não for um mesh collider, tenta pegar pelo renderer
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                return renderer ? renderer.sharedMaterial : null;
            }

            Mesh mesh = meshCollider.sharedMesh;
            int triIndex = hit.triangleIndex;

            // Encontrar o submesh correspondente
            int subMeshIndex = GetSubmeshFromTriangle(mesh, triIndex);

            if (subMeshIndex >= 0 && meshCollider.GetComponent<Renderer>() != null)
            {
                Renderer rend = meshCollider.GetComponent<Renderer>();
                if (subMeshIndex < rend.sharedMaterials.Length)
                    return rend.sharedMaterials[subMeshIndex];
            }

            return null;
        }

        int GetSubmeshFromTriangle(Mesh mesh, int triangleIndex)
        {
            int lookup = triangleIndex * 3;
            int accumulated = 0;

            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                int[] tris = mesh.GetTriangles(i);
                if (lookup < accumulated + tris.Length)
                    return i;
                accumulated += tris.Length;
            }

            return -1;
        }
    }
}
