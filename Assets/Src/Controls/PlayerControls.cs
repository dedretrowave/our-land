using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Src.Controls
{
    public class PlayerControls
    {
        private static PlayerControls _instance;

        public static PlayerControls Instance => _instance ??= new PlayerControls();

        public UnityEvent<Finger> OnFingerUp = new();
        public UnityEvent<Finger> OnFingerMove = new();
        public UnityEvent<Finger> OnFingerDown = new();

        public UnityEvent<Touch> OnTap = new();
        
        private PlayerInput _input;

        private PlayerControls()
        {
            EnhancedTouchSupport.Enable();
            TouchSimulation.Enable();

            _input = new PlayerInput();
            _input.Enable();

            Touch.onFingerUp += OnFingerUp.Invoke;
            Touch.onFingerMove += OnFingerMove.Invoke;
            Touch.onFingerDown += OnFingerDown.Invoke;
        }

        ~PlayerControls()
        {
            Touch.onFingerUp -= OnFingerUp.Invoke;
            Touch.onFingerMove -= OnFingerMove.Invoke;
            Touch.onFingerDown -= OnFingerDown.Invoke;
            
            EnhancedTouchSupport.Disable();
            TouchSimulation.Disable();
        }
    }
}