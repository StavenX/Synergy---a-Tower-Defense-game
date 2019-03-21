using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        //createBullet();
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter % 50 == 0)
        {
            transform.Rotate(Vector3.forward * -90);
            shootBullet();
        }
    }

    private void OnMouseUp()
    {
        shootBullet();
        
    }

    void shootBullet()
    {
        
        //Vector3 v = new Vector3(transform.position.x, transform.position.y);
        bullet = (GameObject)
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

}
