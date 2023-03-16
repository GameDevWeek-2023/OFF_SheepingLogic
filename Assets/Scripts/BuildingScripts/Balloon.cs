using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : Building
{
    
    private GameObject gridScriptAttach;

    void Start()
    {
        gridScriptAttach = GameObject.Find("Terrain");
    }

    void OnCollisionEnter(Collision collision)
    {



    }
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(collision.gameObject);
        gridScriptAttach.GetComponent<GridScript>().IncrementMoney();
    }

}
