using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [Tooltip("Intensidade do efeito parallax (quanto maior, mais forte o efeito)")]
    [Range(0f, 1f)]
    public float parallaxIntensity = 0.1f;
    [Tooltip("Velocidade de suavização do movimento")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.1f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
    }

    void Update()
    {
        // Normaliza a posição do mouse para -1 a 1
        Vector2 mouseNorm = new Vector2(
            (Input.mousePosition.x / Screen.width) * 2 - 1,
            (Input.mousePosition.y / Screen.height) * 2 - 1
        );

        // Calcula o deslocamento de rotação (parallax)
        float maxAngle = parallaxIntensity * 30f; // 30 graus de máximo por padrão
        float rotX = -mouseNorm.y * maxAngle;
        float rotY = mouseNorm.x * maxAngle;
        Quaternion parallaxRot = Quaternion.Euler(rotX, rotY, 0);
        targetRotation = initialRotation * parallaxRot;

        // Suaviza a rotação
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
    }
}
