using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab : Building
{

    void OnCollisionEnter(Collision collision)
    {

        Destroy(collision.gameObject);

    }

}
