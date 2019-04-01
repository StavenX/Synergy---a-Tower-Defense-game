using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour
{
    public Text goldLabel;
    public Text waveLabel;
    public Text enemiesLeftLabel;

    private int wave;
    private int gold;
    private int enemiesLeft;


    public void Start()
    {
        Gold = 1000;
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

}
