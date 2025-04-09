using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public bool isTouching { get; private set; }
    public Vector2? currentTouchPosition { get; private set; }
    
    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += FingerDown;
        Touch.onFingerUp += FingerUp;
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= FingerDown;
        Touch.onFingerUp -= FingerUp;
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
    }

    private void FingerDown(Finger finger)
    {
        isTouching = true;
        currentTouchPosition = finger.screenPosition;
    }

    private void FingerUp(Finger finger)
    {
        isTouching = false;
        currentTouchPosition = finger.screenPosition;
    }
}