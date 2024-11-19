using DG.Tweening;
using UnityEngine;

namespace AI
{
    public class BotScaler : MonoBehaviour
    {
        [SerializeField] private BotCollect botCollect;
        [SerializeField] private GameObject playerObj;
        private bool _scaledBig;
        private bool _scaledSmall;
        private Vector3 MaxScale { get; } = new(10f, 10f, 10f);

        private void OnEnable()
        {
            botCollect.OnCollectFruit += ScaleBot;
            botCollect.OnDestructibleCollect += ScaleBot;
            botCollect.OnPlayerCollect += ScaleBot;
        }

        private void OnDisable()
        {
            botCollect.OnCollectFruit -= ScaleBot;
            botCollect.OnDestructibleCollect -= ScaleBot;
            botCollect.OnPlayerCollect -= ScaleBot;
        }

        private void ScaleBot(float scaleAmount)
        {
            Vector3 current = playerObj.transform.localScale;
            var decreasedAmount = scaleAmount / 50;
            Vector3 newScale = current + new Vector3(decreasedAmount, decreasedAmount, decreasedAmount);
            newScale = Vector3.Min(newScale, MaxScale);

            playerObj.transform.DOScale(newScale, 0.5f);
        }
    }
}