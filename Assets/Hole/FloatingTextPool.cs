using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Hole
{
    public class FloatingTextPool : MonoBehaviour
    {
        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private int poolSize = 50;

        [SerializeField] private Transform textParent;
        private readonly Queue<TextMeshPro> pool = new();

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                var textObj = Instantiate(floatingTextPrefab, textParent);
                textObj.SetActive(false);

                var textComponent = textObj.GetComponent<TextMeshPro>();
                if (textComponent != null)
                {
                    pool.Enqueue(textComponent);
                }
            }
        }
        
        public TextMeshPro GetText()
        {
            var text = pool.Dequeue();
            text.gameObject.SetActive(true);
            return text;
        }
        
        public void ReturnText(TextMeshPro text)
        {
            text.gameObject.SetActive(false); 
            pool.Enqueue(text); 
        }
    }
}