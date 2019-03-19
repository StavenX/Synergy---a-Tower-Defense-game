using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour {

    public GameObject monsterPrefab;
    private GameObject monster; 

    /**
     *  Only allows one monster per tower-spot. If monster is null, no monster is here 
     *  and the placing of a new one will be possible. 
     */
    private bool CanPlaceMonster()
    {
        return monster == null; 
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
    }

}
