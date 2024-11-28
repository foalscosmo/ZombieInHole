using Ui;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Hole
{
    public class MovementWithTransform : MonoBehaviour
    {
        [SerializeField] private Vector2 joystickSize;
        [SerializeField] private FloatingJoystick joystick;
        [SerializeField] private float moveSpeed;

        private Finger movementFinger;
        private Vector2 movementAmount;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += HandleFingerDown;
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += HandleLoseFinger;
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += HandleFingerMove;
        }

        private void OnDisable()
        {
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= HandleFingerDown;
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= HandleLoseFinger;
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= HandleFingerMove;
            EnhancedTouchSupport.Disable();
        }

        private void HandleFingerMove(Finger movedFinger)
        {
            if (movedFinger == movementFinger)
            {
                Vector2 knobPosition;
                var maxMovement = joystickSize.x / 2f;
                var currentTouch = movedFinger.currentTouch;

                if (Vector2.Distance(currentTouch.screenPosition, joystick.RectTransform.anchoredPosition) > maxMovement)
                {
                    knobPosition = (currentTouch.screenPosition - joystick.RectTransform.anchoredPosition).normalized * maxMovement;
                }
                else
                {
                    knobPosition = currentTouch.screenPosition - joystick.RectTransform.anchoredPosition;
                }

                joystick.Knob.anchoredPosition = knobPosition;
                movementAmount = knobPosition / maxMovement;
            }
        }

        private void HandleLoseFinger(Finger lostFinger)
        {
            if (lostFinger == movementFinger)
            {
                movementFinger = null;
                joystick.Knob.anchoredPosition = Vector2.zero;
                joystick.gameObject.SetActive(false);
                movementAmount = Vector2.zero; // Stop movement
            }
        }

        private void HandleFingerDown(Finger touchedFinger)
        {
            if (touchedFinger.screenPosition.x <= Screen.height / 2f)
            {
                movementFinger = touchedFinger;
                movementAmount = Vector2.zero;
                joystick.gameObject.SetActive(true);
                joystick.RectTransform.sizeDelta = joystickSize;
                joystick.RectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
            }
        }

        private Vector2 ClampStartPosition(Vector2 startPosition)
        {
            if (startPosition.x < joystickSize.x / 2)
                startPosition.x = joystickSize.x / 2;

            if (startPosition.y < joystickSize.y / 2)
                startPosition.y = joystickSize.y / 2;
            else if (startPosition.y > Screen.height - joystickSize.y / 2)
                startPosition.y = Screen.height - joystickSize.y / 2;

            return startPosition;
        }

        private void FixedUpdate()
        {
            // Move only if there's active touch input
            if (movementFinger == null || movementAmount.magnitude <= 0.1f)
                return;

            var targetDirection = new Vector3(movementAmount.x, 0, movementAmount.y).normalized;
            var transform1 = transform;
            var movePosition = transform1.position + targetDirection * (moveSpeed * Time.fixedDeltaTime);
            transform1.position = movePosition;
        }
    }
}