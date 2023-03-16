using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab : Building
{

    private GameObject gridScriptAttach;

    void Start()
    {
        gridScriptAttach = GameObject.Find("Terrain");
    }

    void OnCollisionEnter(Collision collision)
    {

        Destroy(collision.gameObject);
        gridScriptAttach.GetComponent<GridScript>().IncrementResearch();

    }

}