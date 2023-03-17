using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : Building
{
    [SerializeField] Animator animator;
    private void Start()
    {

        animator.Play("PlaceObject");
    }
}
