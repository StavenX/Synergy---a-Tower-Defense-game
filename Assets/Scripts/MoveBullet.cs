using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = (int) transform.rotation.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime*2, Camera.main.transform);
        deleteBullet();
    }

    void deleteBullet()
    {
        int x = (int) transform.position.x;
        int y = (int)transform.position.y;

        if (x > 10 || x < -10 || y > 10 || y < -10) {
            Destroy(this);
        }
    }
}
