using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform _centerPoint;
    [FormerlySerializedAs("_touchRange")] [SerializeField] private float _groundCheckRange = 2f; // Quanto è la distanza che delimita se il player sta toccando terreno
    [SerializeField] private float _maxAngle = 25f;
    [SerializeField] private float _gravityTime = 0.5f; // Quanto tempo impiega la gravità ad avere effeto una volta ruotato il mondo
    [SerializeField] private float _gravity = 1f;
    [SerializeField] private Vector3 _gravityDirection = Vector3.down;
    
    [Header("JUMP DATA")]
    [SerializeField] private float _jumpForce = 5f;
    
    
    
    

    private Transform _cameraTransform;
    
    private Vector3 _playerInput;
    private Vector3 _initialFWD;
    
    private bool _isGrounded = false;
    private bool _isJumping = true;
    private float _velocityY = 0f;
    

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _initialFWD = transform.forward;
    }

    private void OnEnable()
    {
        InputController.OnMove += SetPlayerInput;
        InputController.OnJumping += Jump;
    }

    private void OnDisable()
    {
        InputController.OnMove -= SetPlayerInput;
        InputController.OnJumping -= Jump;
    }
    
    
    private void Update()
    {
       
        CheckFall(); 
        if(!_isGrounded)
            Fall();
        
        Move();
        
            
        
        
        // _timer += Time.deltaTime;
        //
        // if(_timer <= _gravityTime)
        //     return;
        //
        // _timer = 0f;
        // _hasRotated = false;
        
        
    }
    
    

    private void CheckFall()
    {
        Ray ray = new Ray(_centerPoint.position , _gravityDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _groundCheckRange))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle > _maxAngle)
            {
                _isGrounded = false;
                return;
            }

            if (!_isJumping)
                _velocityY = 0f;
            
            
            float snapY = hit.point.y + _groundCheckRange;
            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, snapY, 20f * Time.deltaTime);
            transform.position = pos;
            Vector3 fwd = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;
            if(fwd != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(fwd , hit.normal);
            
            _isJumping = false;
            _isGrounded = true;
            
        } else _isGrounded = false;
        
        
        
    }
    

    private void Fall()
    {
        
        Debug.Log("Falling");
        // transform.position += _gravityDirection * (_gravity * Time.deltaTime);
        //
        
        _velocityY += -_gravity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(_initialFWD , Vector3.up);
    }


    
    private void SetPlayerInput(Vector3 input) => _playerInput = input;
    
    private void Move()
    {
        
        Vector3 direction = Vector3.zero;
        if (_playerInput.sqrMagnitude > 0.0001f)
        {
            direction = _cameraTransform.right * _playerInput.x + _cameraTransform.forward * _playerInput.y;
            if (direction.sqrMagnitude > 1f)
                direction.Normalize();
        }

        transform.position += (direction * _speed + Vector3.up * _velocityY) * Time.deltaTime;

    }

    private void Jump()
    {
        if(!_isGrounded)
            return;
        
        Debug.Log("Jumping");
        _isJumping = true;
        _velocityY = _jumpForce;
    }
    
    
    private void OnDrawGizmos()
    {

        
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(_centerPoint.position, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_centerPoint.position, _gravityDirection * _groundCheckRange);
        
        Gizmos.color = Color.green;
        RaycastHit hit;
        if (Physics.Raycast(_centerPoint.position, _gravityDirection, out hit, _groundCheckRange))
        {
            Gizmos.DrawSphere(hit.point, 0.2f);
        }
        
    }
}
