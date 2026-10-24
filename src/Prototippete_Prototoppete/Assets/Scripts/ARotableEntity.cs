using UnityEngine;


public abstract class ARotableEntity : MonoBehaviour
{
    
    [SerializeField] private float _rotationAngle;
    [SerializeField] private Transform _rotationPivot;
    
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        
        if(_rotationPivot == null)
            _rotationPivot = transform;
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
        transform.RotateAround(_rotationPivot.position, direction, _rotationAngle);
    }

    protected virtual void ResetRotation()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }
}
