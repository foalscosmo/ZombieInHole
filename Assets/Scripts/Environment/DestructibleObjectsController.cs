using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class DestructibleObjectsController : MonoBehaviour
    {
        [SerializeField] private List<BoxCollider> bigObjectsColl = new();
        [SerializeField] private List<BoxCollider> smallObjectsColl = new();

        private void Awake()
        {
            SetCollidersTriggerBig();
            SetCollidersTriggerToMedium();
        }
        
        private void SetCollidersTriggerBig()
        {
            foreach (var colliders in bigObjectsColl) colliders.isTrigger = true;
        }
        
        private void SetCollidersTriggerToMedium()
        {
            foreach (var colliders in smallObjectsColl) colliders.isTrigger = true;
        }
    }
}