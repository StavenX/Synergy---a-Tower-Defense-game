using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWave : MonoBehaviour
{

    public string buttonText;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = "START";


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextWave()
    {
        gameObject.GetComponentInChildren<Text>().enabled = false;
        gameObject.GetComponent<Image>().enabled = false;
        GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>().gamePaused = false;
    }
        
}
