using System;
using UnityEngine;

public class RankScore : MonoBehaviour
{
    public string playerName; 
    public float score;

    private void Start()
    {
        playerName = gameObject.name;
    }

    public RankScore(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}