using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : Building
{

    void OnCollisionEnter(Collision collision)
    {

        Destroy(collision.gameObject);

    }

}
