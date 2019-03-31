using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour {

    public GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehaviour gameManager; 

    /**
     *  Only allows one monster per tower-spot. If monster is null, no monster is here 
     *  and the placing of a new one will be possible. 
     */
    private bool CanPlaceMonster()
    {
        /*
        //doesn't work @EDVIN pls fix
        int cost = monsterPrefab.GetComponent<MonsterData>
            ().levels[0].cost;

        return monster == null && gameManager.Gold >= cost; 
        */
        return monster == null;
    }

    private bool CanUpgradeMonster()
    {
        if (monster != null)
        {
            MonsterData monsterData = monster.GetComponent<MonsterData>();
            MonsterLevel nextLevel = monsterData.GetNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost; 
            }
        }

        return false;
    }

    /**
     *  Places a monster on the location if there is an open spot. 
     */
    private void OnMouseUp()
    {
        if (CanPlaceMonster())
        {
            monster = (GameObject)
                Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip); 
        }
        else if (CanUpgradeMonster())
        {
            monster.GetComponent<MonsterData>().IncreaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= monster.GetComponent<MonsterData>
                ().CurrentLevel.cost; 
        }
    }

    private void Start()
    {
        gameManager =
            GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
    }

}
