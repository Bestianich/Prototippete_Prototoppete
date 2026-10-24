using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rotationAngle = 90;
    [SerializeField] private Transform _rotationPivot;
    
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Awake()
    {
        if(_camera == null)
            _camera = Camera.main;
        
        _initialPosition = _camera.transform.position;
        _initialRotation = _camera.transform.rotation;
    }

    private void OnEnable()
    {
        InputController.OnTurnCamera += TurnCamera;
        InputController.OnResetCamera += ResetCamera;
    }

    private void OnDisable()
    {
        InputController.OnTurnCamera -= TurnCamera;
        InputController.OnResetCamera -= ResetCamera;
    }
    public void TurnCamera(Vector3 direction)
    {
        _camera.transform.RotateAround(_rotationPivot.position, direction, _rotationAngle);
    }

    public void ResetCamera()
    {
        _camera.transform.position = _initialPosition;
        _camera.transform.rotation = _initialRotation;
    }
}
