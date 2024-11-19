using System;
using System.Collections.Generic;
using AI;
using Ui;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class RankContainer : MonoBehaviour
    {
        [SerializeField] private List<RankScore> rankScores = new();
        [SerializeField] private RankScore playerRank;

        public List<RankScore> RankScores
        {
            get => rankScores;
            set => rankScores = value;
        }
        
        private void Start()
        {
            rankScores.Add(playerRank);
        }
    }
}