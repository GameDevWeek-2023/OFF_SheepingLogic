using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : Building
{

    public GameObject colorful_sheep;

    void OnTriggerEnter(Collider collision)   
    {

        GetComponent<Animation>().Play();
        Transform t = collision.gameObject.transform;
        Destroy(collision.gameObject);
        Instantiate(colorful_sheep, t.position, t.rotation);

    }

}
