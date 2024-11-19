using UnityEngine;

namespace AI
{
    public class BotIdentifier : MonoBehaviour
    {
        [SerializeField] private int botIndex;
        public int BotIndex
        {
            get => botIndex;
            set => botIndex = value;
        }
    }
}