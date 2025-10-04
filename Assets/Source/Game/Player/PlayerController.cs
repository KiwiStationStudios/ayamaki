using UnityEngine;

namespace Ayamaki.Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private Transform mainCamera;
        [SerializeField] private float normalSpeed = 4.5f;
        [SerializeField] private float runningMultiplier = 7f;
        [SerializeField] private bool running = false;
        [SerializeField] private bool crouching = false;
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] private float minRotY = -80f;
        [SerializeField] private float maxRotY = 80f;
        [SerializeField]private float gravity = 9.8f;
        [SerializeField]private float playerSpeed = 5f;
        [SerializeField]private float jumpHeight = 5f;
        private float xRotation = 0f; // câmera olhar pra cima/baixo
        private float yRotation = 0f; // player girar pro lado
        private float _yVelocity = 0f;

        private CharacterController charController;
        void Start()
        {
            charController = GetComponent<CharacterController>();
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            Vector3 movePlayer = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            Vector3 moveDirection = transform.TransformDirection(movePlayer);

            // Girar player no eixo Y (esquerda/direita)
            yRotation += mouseX;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minRotY, maxRotY); // limita a inclinação
            mainCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            crouching = Input.GetKey(KeyCode.LeftControl) && !running;
            running = Input.GetKey(KeyCode.LeftShift) && !crouching;

            float currentSpeed = running ? normalSpeed * runningMultiplier : normalSpeed;
            Vector3 velocity = moveDirection * currentSpeed;

            if (charController.isGrounded)
                if (Input.GetKeyDown(KeyCode.Space))
                    _yVelocity = jumpHeight; // inicia o pulo
                else
                    _yVelocity = -2f; // mantém player preso no chão
            else
                _yVelocity -= gravity * Time.deltaTime; // aplica gravidade

            velocity.y = _yVelocity;

            charController.Move(velocity * Time.deltaTime);
        }

        void OnDrawGizmos()
        {
            if (mainCamera == null) return;

            Gizmos.color = Color.red;

            // posição de origem
            Vector3 origin = mainCamera.position;

            // direção "neutra" (olhando reto pra frente)
            Vector3 forward = transform.forward;

            // direções dos limites
            Quaternion minRot = Quaternion.AngleAxis(minRotY, transform.right);
            Quaternion maxRot = Quaternion.AngleAxis(maxRotY, transform.right);

            Vector3 minDir = minRot * forward;
            Vector3 maxDir = maxRot * forward;

            // desenha linhas mostrando os limites
            Gizmos.DrawRay(origin, minDir * 2f);
            Gizmos.DrawRay(origin, maxDir * 2f);
        }
    }
}
