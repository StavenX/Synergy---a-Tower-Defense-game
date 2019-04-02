using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour
{
    public Text goldLabel;
    private int gold;

    public Text waveLabel;
    private int wave;

    public Text enemiesLeftLabel;
    private int enemiesLeft;

    public Text healthLabel;
    private int health;

    public Text gameOverLabel;
    private bool gameOver;


    private void Start()
    {
        Gold = 1000;
        Health = 10;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            GameOver = true;
            Debug.Log("gameover: " + gameOverLabel.text);
            gameObject.GetComponent<GameOver>().RestartLevel();
        }
    }

    public int Gold
    {
        get
        {
            return gold; 
        }
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "GOLD: " + gold; 
        }
    }

    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            waveLabel.GetComponent<Text>().text = "WAVE: " + wave;
        }
    }

    public int EnemiesLeft
    {
        get
        {
            return enemiesLeft;
        }
        set
        {
            enemiesLeft = value;
            enemiesLeftLabel.GetComponent<Text>().text = "ENEMIES LEFT: " + enemiesLeft;
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthLabel.GetComponent<Text>().text = "HEALTH: " + health;
        }
    }


    public bool GameOver
    {
        get { return gameOver; }
        set
        {
            gameOver = value;
            gameOverLabel.GetComponent<Text>().text = "GAME !UNDER";
        }
    }
}
