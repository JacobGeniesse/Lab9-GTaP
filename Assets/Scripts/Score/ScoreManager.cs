using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class ScoreManager : MonoBehaviour, IObserver
{
    [HideInInspector] public float score; //float representing the current score

    private TextMeshProUGUI scoreText; //The TMPro asset that displays the current score

    public static ScoreManager instance; //singleton instance of this script

    //Singleton setup
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

    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>(); //Assign the score text reference
    }

    //Func for updating the active score
    public void UpdateScore(float addedAmount)
    {
        score += addedAmount;
    }

    //Function for updating the UI element which displays the current score
    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }

    //Observer update function for updating the UI
    public void ListenTrigger(IScore masterScore)
    {
        UpdateUI();
    }
}
