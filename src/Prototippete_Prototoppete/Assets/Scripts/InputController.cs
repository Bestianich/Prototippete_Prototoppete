using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour , FlippeteInput.ICameraInputActions
{
    public delegate void TurnCameraAction(Vector3 direction);
    public static event TurnCameraAction OnTurnCamera;

    public delegate void ResetCameraAction();
    public static event ResetCameraAction OnResetCamera;
    
    public static InputController Instance;


    private FlippeteInput _controls;
    
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new FlippeteInput();
            _controls.CameraInput.SetCallbacks(this);
        }
        _controls.CameraInput.Enable();
    }

    public void OnDisable()
    {
        _controls.CameraInput.Disable();
    }
    
    public void OnTurnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTurnCamera?.Invoke(new Vector3(0 , 0 , 1));
    }

    public void OnTurnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTurnCamera?.Invoke(new Vector3(0 , 0 , -1));
    }

    public void OnTurnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTurnCamera?.Invoke(new Vector3(1 , 0 , 0));
    }

    public void OnTurnDown(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTurnCamera?.Invoke(new Vector3(-1, 0 , 0));
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnResetCamera?.Invoke();
    }
}
