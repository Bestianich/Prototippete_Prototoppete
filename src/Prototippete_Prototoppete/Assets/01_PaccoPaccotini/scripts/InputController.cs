using UnityEngine;
using UnityEngine.InputSystem;

namespace PaccoPaccotini
{
    public class InputController : MonoBehaviour , PaccoInput.ICarInputActions , PaccoInput.IGroundInputActions
    {
        
        #region Delegates
        
        public delegate void OnSteerDelegate(Vector2 input);
        public static OnSteerDelegate OnSteerAction;
        
        public delegate void OnBoostDelegate(bool pressed);
        public static OnBoostDelegate OnBoostAction;
        
        public delegate void OnWalkDelegate(Vector2 input);
        public static OnWalkDelegate OnWalkAction;
        
        public delegate void OnGrabDelegate();
        public static OnGrabDelegate OnGrabAction;
        
        public delegate void OnJumpDelegate();
        public static OnJumpDelegate OnJumpAction;
        
        #endregion

        

        private void Awake()
        {
            
        }
        
        
        
        #region InputEvents
        
        public void OnSteer(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnSteerAction?.Invoke(context.ReadValue<Vector2>());
            else if(context.canceled)
                OnSteerAction?.Invoke(Vector2.zero);
        }

        public void OnBoost(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnBoostAction?.Invoke(true);
            else if(context.canceled)
                OnBoostAction?.Invoke(false);
        }

        public void OnWalk(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnWalkAction?.Invoke(context.ReadValue<Vector2>());
            if(context.canceled)
                OnWalkAction?.Invoke(Vector2.zero);
        }

        public void OnGrab(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnGrabAction?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnJumpAction?.Invoke();
        }
        
        #endregion
    }
}
