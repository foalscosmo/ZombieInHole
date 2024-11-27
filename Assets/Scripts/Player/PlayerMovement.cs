using Ui;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
       [SerializeField] private Vector2 joystickSize;
        [SerializeField] private FloatingJoystick joystick;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private Rigidbody rb;
        
        private Finger _movementFinger;
        private Vector2 _movementAmount;
        private Vector3 _lastDirection;

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerDown += HandleFingerDown;
            ETouch.Touch.onFingerUp += HandleLoseFinger;
            ETouch.Touch.onFingerMove += HandleFingerMove;
        }

        private void OnDisable()
        {
            ETouch.Touch.onFingerDown -= HandleFingerDown;
            ETouch.Touch.onFingerUp -= HandleLoseFinger;
            ETouch.Touch.onFingerMove -= HandleFingerMove;
            EnhancedTouchSupport.Disable();
        }

        private void HandleFingerMove(Finger movedFinger)
        {
            if (movedFinger == _movementFinger)
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
                _movementAmount = knobPosition / maxMovement;
                _lastDirection = new Vector3(_movementAmount.x, 0, _movementAmount.y).normalized;
            }
        }

        private void HandleLoseFinger(Finger lostFinger)
        {
            if (lostFinger == _movementFinger)
            {
                _movementFinger = null;
                joystick.Knob.anchoredPosition = Vector2.zero;
                joystick.gameObject.SetActive(false);
                _movementAmount = Vector2.zero;
            }
        }

        private void HandleFingerDown(Finger touchedFinger)
        {
            if (touchedFinger.screenPosition.x <= Screen.height / 2f)
            {
                _movementFinger = touchedFinger;
                _movementAmount = Vector2.zero;
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
            var targetDirection = _movementAmount.magnitude > 0.1f 
                ? new Vector3(_movementAmount.x, 0, _movementAmount.y).normalized 
                : _lastDirection;

            if (!(targetDirection.magnitude > 0.1f)) return;
            var movePosition = rb.position + targetDirection * (moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);

            var targetRotation = Quaternion.LookRotation(targetDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));
        }
    }
}
