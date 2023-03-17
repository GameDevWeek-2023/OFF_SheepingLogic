using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab : Building
{
    [SerializeField] Animator animator;

    private GameObject gridScriptAttach;

    public IntValue science;

    void Start()
    {
        gridScriptAttach = GameObject.Find("Terrain");
        animator.Play("PlaceObject");
    }

    void OnCollisionEnter(Collision collision)
    {



    }
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(collision.gameObject);
        science.Increment();
        animator.Play("ReciveObject");
    }

}
