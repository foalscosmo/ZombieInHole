using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI
{
    public class BotMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float directionChangeInterval = 3f;
        [SerializeField] private float rotationSpeed = 15f;
        [SerializeField] private float collisionCooldown = 1f; // Cooldown after a collision

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }
        private Vector3 _movementDirection;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private LayerMask boundLayer;
        private bool canChangeDirection = true; // Direction change cooldown flag

        private void Awake()
        {
            SetRandomDirection();
            StartCoroutine(ChangeDirectionRoutine());
        }

        private void FixedUpdate()
        {
            Vector3 movePosition = rb.position + _movementDirection * (moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(movePosition);

            if (_movementDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_movementDirection);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }

        private IEnumerator ChangeDirectionRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(directionChangeInterval);
                if (canChangeDirection)
                {
                    SetRandomDirection();
                }
            }
        }

        private void SetRandomDirection()
        {
            _movementDirection = new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f)
            ).normalized;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & boundLayer) != 0 && canChangeDirection)
            {
                _movementDirection = -_movementDirection; // Reverse direction
                StartCoroutine(CollisionCooldownRoutine());
            }
        }

        private IEnumerator CollisionCooldownRoutine()
        {
            canChangeDirection = false; // Disable direction changes
            yield return new WaitForSeconds(collisionCooldown);
            canChangeDirection = true; // Re-enable direction changes
        }
    }
}