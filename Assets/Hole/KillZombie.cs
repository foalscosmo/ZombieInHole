using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Hole
{
    public class KillZombie : MonoBehaviour
    {
        [SerializeField] private LayerMask botBody;
        [SerializeField] private FloatingTextPool floatingTextPool;

        public int ZombieValue { get; private set; }

        public event Action<int> OnZombieKill;

        private void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & botBody) != 0)
            {
                ZombieValue = Mathf.RoundToInt(other.transform.localScale.x);
                OnZombieKill?.Invoke(ZombieValue);
                ShowFloatingText(ZombieValue);
            }
        }

        private void ShowFloatingText(float value)
        {
            var text = floatingTextPool.GetText();
            text.transform.position =
                new Vector3(transform.position.x - 1f, transform.position.y + 7f, transform.position.z);
            text.text = "+" + value;

            StartCoroutine(ReturnTextToPoolAfterDelay(text, 1f));
        }

        private IEnumerator ReturnTextToPoolAfterDelay(TextMeshPro text, float delay)
        {
            yield return new WaitForSeconds(delay);
            floatingTextPool.ReturnText(text);
        }
    }
}