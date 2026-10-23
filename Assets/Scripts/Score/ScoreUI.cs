using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + Score.instance.score;
    }


    public void ListenTrigger(IScore masterScore)
    {
        UpdateUI();
    }

}
