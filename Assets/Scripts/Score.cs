using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    [SerializeField] private float score;


    private List<IObserver> observers = new List<IObserver>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
