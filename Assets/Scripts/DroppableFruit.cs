using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DroppableFruit : MonoBehaviour
{
    // public float upForce; 
    public float sideForce = 10f; 
    public float throwDuration;
    [SerializeField] private BoxCollider boxCollider;
    
    public void Throw(Vector3 origin)
    {
        origin.y = 1f;
        StartCoroutine(ThrowCoroutine(origin));
    }

    private IEnumerator ThrowCoroutine(Vector3 origin)
    {
        float elapsedTime = 0f;

        Vector3 randomOffset = new Vector3(
            Random.Range(-sideForce, sideForce),
            0, 
            Random.Range(-sideForce, sideForce)
        );

        Vector3 targetPosition = origin + randomOffset;
        targetPosition.y = 1f; 

        while (elapsedTime < throwDuration)
        {
            float t = elapsedTime / throwDuration;

            Vector3 positionXZ = Vector3.Lerp(origin, targetPosition, t);

           // float arcHeight = Mathf.Sin(t * Mathf.PI) * upForce;
           positionXZ.y = Mathf.Lerp(origin.y, targetPosition.y, t);// + arcHeight;

            transform.position = positionXZ;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        boxCollider.enabled = true;
        transform.position = targetPosition;
    }
}