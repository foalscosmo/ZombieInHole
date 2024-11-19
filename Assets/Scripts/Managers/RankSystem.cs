using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class RankSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] rankScoreTexts;
        [SerializeField] private RankContainer rankContainer;

        private void Start()
        {
            UpdateRankings();
        }

        private void Update()
        {
            UpdateRankings();
        }

        private void UpdateRankings()
        {
            rankContainer.RankScores.Sort((x, y) => y.score.CompareTo(x.score));
            for (var i = 0; i < rankScoreTexts.Length; i++)
            {
                if (i < rankContainer.RankScores.Count)
                {
                    rankScoreTexts[i].text =
                        $" {i + 1}. {rankContainer.RankScores[i].playerName}: {rankContainer.RankScores[i].score}";
                }
            }
        }
    }
}