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

    private int enemiesKilled;

    private LinkedList<GameObject> enemies;


    private void Start()
    {
        Gold = 1000;
        Health = 40;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            GameOver = true;
            //Debug.Log("gameover: " + gameOverLabel.text);
            StopCoroutine(gameObject.GetComponent<CreateEnemies>().spawnEnemies());
            //gameObject.GetComponent<GameOver>().RestartLevel();
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


    public bool GameOver
    {
        get { return gameOver; }
        set
        {
            gameOver = value;
            gameOverLabel.transform.position = Vector3.MoveTowards(gameOverLabel.transform.position, new Vector3(450, 0, 0), Time.deltaTime * 500);
            //gameOverLabel.GetComponent<Text>().text = "GAME !UNDER";
            StartCoroutine(Wait(5));
            //gameOverLabel.GetComponent<Text>().color = Color.white;
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public int EnemiesKilled
    {
        get { return enemiesKilled; }
        set
        {
            enemiesKilled = value;
        }
    }

    public LinkedList<GameObject> Enemies
    {
        get
        {
            var list = GameObject.FindGameObjectsWithTag("Enemy");
            enemies = new LinkedList<GameObject>(list);
            return enemies;
        }
    }
}
