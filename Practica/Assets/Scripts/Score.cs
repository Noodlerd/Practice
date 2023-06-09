using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentScore;
    public int bestScore;

    [SerializeField] private TextMeshProUGUI scoreText =null;
    [SerializeField] private TextMeshProUGUI bestScoreText=null;

    public static Score Instance { get; set; }

    private void Start()
    {
        Instance = this;
        // Загрузка лучшего счета из сохранения
        bestScore = PlayerPrefs.GetInt("BestScore",0);

        if (bestScoreText != null)
            bestScoreText.text = "Best " + bestScore;

    }

    private void TryToSaveBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            // Сохранение лучшего счета
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void PlusCurrentScore()
    {
       currentScore+=1;
       scoreText.text = currentScore.ToString();
       TryToSaveBestScore();
    }
}
