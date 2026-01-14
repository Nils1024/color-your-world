using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public Camera playerCamera;
        public float walkSpeed = 6f;
        public float runSpeed = 12f;
        public float jumpPower = 7f;
        public float gravity = 10f;
        public float lookSpeed = 1f;
        public float lookXLimit = 45f;

        private Vector3 _moveDirection = Vector3.zero;
        private float _rotationX;
        private CharacterController _characterController;
        private const bool CanMove = true;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            bool isRunning = keyboard.leftShiftKey.isPressed;

            float verticalInput =
                (keyboard.wKey.isPressed ? 1 : 0) -
                (keyboard.sKey.isPressed ? 1 : 0);

            float horizontalInput =
                (keyboard.dKey.isPressed ? 1 : 0) -
                (keyboard.aKey.isPressed ? 1 : 0);

            float curSpeedX = (isRunning ? runSpeed : walkSpeed) * verticalInput;
            float curSpeedY = (isRunning ? runSpeed : walkSpeed) * horizontalInput;

            float movementDirectionY = _moveDirection.y;
            _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Jump
            if (keyboard.spaceKey.wasPressedThisFrame && CanMove && _characterController.isGrounded)
            {
                _moveDirection.y = jumpPower;
            }
            else
            {
                _moveDirection.y = movementDirectionY;
            }

            // Gravity
            if (!_characterController.isGrounded)
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }

            _characterController.Move(_moveDirection * Time.deltaTime);

            // Mouse look
            if (CanMove)
            {
                Vector2 mouseDelta = mouse.delta.ReadValue();

                _rotationX += -mouseDelta.y * lookSpeed;
                _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);

                playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, mouseDelta.x * lookSpeed, 0);
            }
        }
    }
}