using UnityEngine;
using UnityEngine.InputSystem;

namespace PaccoPaccotini
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed;
        private Rigidbody _rb;
        private Camera _camera;
        
        private Vector2 _movementInput;
        
        
        public void GetPlayerMovementInput(InputAction.CallbackContext ctx) => _movementInput = ctx.ReadValue<Vector2>();


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if(_movementInput != Vector2.zero)
                Move();
        }

        private void Move()
        {
            Vector3 forward = _camera.transform.forward.normalized;
            Vector3 right = _camera.transform.right.normalized;
            forward.y = 0;
            right.y = 0;
            
            Vector3 dir = (right * _movementInput.x + forward * _movementInput.y).normalized;
            
            transform.position += dir * (_playerSpeed * Time.deltaTime);
        }
    }
}