using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class Score : MonoBehaviour, IObserver
{
    public float score;

    [SerializeField] private TextMeshProUGUI scoreText;

    public static Score instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple Score Scripts in one scene!");
            Destroy(this);
        }
    }


    private List<IObserver> listeners = new List<IObserver>();


    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(float addedAmount)
    {
        if(score + addedAmount > 0)
        {
            score += addedAmount;
        }
        else
        {
            score = 0;
        }
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void ListenTrigger(IScore masterScore)
    {
        UpdateUI();
    }
}
