using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //public Transform target;
    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(8.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime*5);
        if (transform.position == target)
        {
            Destroy(gameObject);
        }
    }
}
