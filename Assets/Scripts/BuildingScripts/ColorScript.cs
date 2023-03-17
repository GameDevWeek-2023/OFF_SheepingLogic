using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : Building
{

    public GameObject colorful_sheep;

    void OnTriggerEnter(Collider collision)   
    {

        if (collision.gameObject.tag == "weissesSchaf" || collision.gameObject.tag == "schwarzesSchaf")
        {
            //GetComponent<Animation>().Play();
            Transform t = collision.gameObject.transform;
            Destroy(collision.gameObject);
            Instantiate(colorful_sheep, t.position, t.rotation);
        }
    }

}
