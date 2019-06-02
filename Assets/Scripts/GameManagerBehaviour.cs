using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This class keeps track of a lot of the values used in the game, 
 * by multiple classes, such as gold or wave number
 * */
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

    public bool gamePaused;

    private int enemiesKilled;

    private void Start()
    {
        Gold = 5000;
        Health = 40;
        gamePaused = false;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            StopCoroutine(gameObject.GetComponent<CreateEnemies>().spawnEnemies());
            StartCoroutine(GameOver());
        }
        else
        {
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

    /**
     * Stops the game
     * */
    public IEnumerator GameOver()
    {
        Resources.FindObjectsOfTypeAll<GameOver>();
        GameObject.FindGameObjectWithTag("GameOver").GetComponent<Text>().color = new Color(255,255,255);
        //waits 5 seconds after "GAME OVER" to restart the game
        yield return wait(5);
        gameObject.GetComponent<GameOver>().RestartLevel();
    }

    public int EnemiesKilled
    {
        get { return enemiesKilled; }
        set
        {
            enemiesKilled = value;
        }
    }

    public IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
