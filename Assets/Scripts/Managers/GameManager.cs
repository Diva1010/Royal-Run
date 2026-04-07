using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool isGameOver = false;

    public bool IsGameOver => isGameOver;    //public bool IsGameOver { get { return isGameOver; }}

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
       DecreaseTime();
    }

    public void IncreaseTimer(float amount)
    {
        timeLeft += amount;
    }


    private void DecreaseTime()
    {
        if (isGameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0f)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
}
