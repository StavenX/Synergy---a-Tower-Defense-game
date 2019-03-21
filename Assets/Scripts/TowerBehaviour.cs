using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;
    private int counter;


    // The target marker.
    public Transform target;

    // Angular speed in radians per sec.
    float speed;
    float rotationAmount;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;

        //high speed means towers instantly turns to target, lower speed means they turn slowly
        //(if using Time.deltaTime * speed)
        //speed = 200.0f;

        //amount a tower rotates towards target every frame
        //1.0 = 100%, 0.5 = 50% etc.
        rotationAmount = 1.0f;

        //sets rotation target to a placeholder enemy object on the map
        target = GameObject.Find("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;

        //reduces amount of work per update to reduce strain on computer
        if (counter % 50 == 0)
        {
            //rotates towers 90 degrees
            //transform.Rotate(Vector3.forward * -90);


            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            //Slerp or RotateTowards methods
            
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationAmount);
            

            shootBullet();
        }
    }

    /*
     * Fires a bullet when a tower is clicked
     * Debug use mostly
     * */
    private void OnMouseUp()
    {
        shootBullet();
        
    }

    /**
     * Tower shoots a bullet
     * */
    void shootBullet()
    {
        //use v as second constructor parameter for bullet to place bullet at v
        //probably remove this
        //Vector3 v = new Vector3(transform.position.x, transform.position.y);

        //new bullet spawns on top of tower
        bullet = (GameObject)
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

}
