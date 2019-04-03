using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectTowerButton : MonoBehaviour
{
    public GameObject buttonTower;

    public void ChangeSelectedTower()
    {
        Debug.Log("Selected " + buttonTower.name);
        PlaceTower.monsterPrefab = buttonTower;
    }
}