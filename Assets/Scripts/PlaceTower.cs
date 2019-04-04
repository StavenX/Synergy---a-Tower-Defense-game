using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour {

    public static GameObject monsterPrefab;
    private GameObject monster;
    private GameManagerBehaviour gameManager;
    public int towerCost = 500;

    private bool printedReason = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
    }

    /**
     *  Only allows one monster per tower-spot. If monster is null, no monster is here 
     *  and the placing of a new one will be possible. 
     */
    private bool CanPlaceMonster()
    {
        int cost = monsterPrefab.GetComponent<MonsterData>
            ().levels[0].cost;

        return monster == null && gameManager.Gold >= cost;
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
            else
            {
                Debug.Log("Tower already fully upgraded");
                printedReason = true;
            }
        }

        return false;
    }

    /**
     *  Places a monster on the location if there is an open spot. 
     */
    private void OnMouseUp()
    {
        if (monsterPrefab == null)
        {
            monsterPrefab = (GameObject) Resources.Load("Prefabs/Monster", typeof(GameObject));
        }
        else
        {
            if (CanPlaceMonster())
            {
                monster = (GameObject)
                    Instantiate(monsterPrefab, transform.position, Quaternion.identity);
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);
                spendGoldOn(monster);
            }
            else if (CanUpgradeMonster())
            {
                monster.GetComponent<MonsterData>().IncreaseLevel();
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);
                spendGoldOn(monster);
            }
            else if (!printedReason)
            {
                Debug.Log("You don't have enough gold!");
            }
        }
    }

    private void spendGoldOn(GameObject g)
    {
        gameManager.Gold -= monster.GetComponent<MonsterData>().CurrentLevel.cost;
        if (gameManager.Gold < 0) Debug.Log("woops negative gold");
    }
}
