using UnityEngine;


public abstract class ARotableEntity : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed = 0.1f;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private Transform _rotationPivot;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    
    private Quaternion _targetRotation;
    
    private bool _isRotating = false;

    private float _timer = 0f;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        
        if(_rotationPivot == null)
            _rotationPivot = transform;
    }

    private void Update()
    {
        if(!_isRotating)
            return;
        
        _timer += Time.deltaTime;
        
        
        float t = _timer * _rotationSpeed;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, t);

        
        if (Quaternion.Angle(transform.rotation, _targetRotation) < 0.5f)
        {
            transform.rotation =  _targetRotation;
            _isRotating = false;
            _timer = 0f;
            _targetRotation = Quaternion.identity;
        }
        
    }
        
    private void OnEnable()
    {
        InputController.OnTurnCamera += Rotate;
        InputController.OnResetCamera += ResetRotation;
    }

    private void OnDisable()
    {
        InputController.OnTurnCamera -= Rotate;
        InputController.OnResetCamera -= ResetRotation;
    }
    protected virtual void Rotate(Vector3 direction)
    {
        if(_isRotating)
            return;
        
        direction *= _rotationAngle;
        _targetRotation = Quaternion.Euler( direction) * transform.rotation;
        
        _isRotating = true;
    }

    protected virtual void ResetRotation()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }
}
