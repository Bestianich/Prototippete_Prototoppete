using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour , FlippeteInput.IPlayerInputActions
{
    public delegate void TurnCameraAction(Vector3 direction);
    public static event TurnCameraAction OnTurnCamera;

    public delegate void ResetCameraAction();
    public static event ResetCameraAction OnResetCamera;
    
    public delegate void MoveAction(Vector3 input);
    
    public static event MoveAction OnMove;
    
    
    
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
            _controls.PlayerInput.SetCallbacks(this);
        }
        _controls.PlayerInput.Enable();
    }

    public void OnDisable()
    {
        _controls.PlayerInput.Disable();
    }
    
    public void OnTurnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTurnCamera?.Invoke(new Vector3(0 , 0 , -1));
    }

    public void OnTurnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnTurnCamera?.Invoke(new Vector3(0 , 0 , 1));
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

    public void OnMovement(InputAction.CallbackContext context)
    { 
        if(context.performed)
            OnMove?.Invoke(context.ReadValue<Vector2>());
        if(context.canceled)
            OnMove?.Invoke(Vector2.zero);
    }
}
