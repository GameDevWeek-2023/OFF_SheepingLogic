using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PeriodicSpawner : Spawner
{

    float last_spawn_time;
    bool playedAnimation = false;
    
    public GameObject spawn_object;
    [SerializeField] Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        last_spawn_time = Time.time;
        animator.Play("PlaceObject");
    }

    // Update is called once per frame
    void Update()
    {

        float t = Time.time;
        
        if (t - last_spawn_time > spawn_delay-0.15f&&!playedAnimation)
        {
            animator.Play("SpawnObject");
            playedAnimation = true;
        }

        if (t - last_spawn_time > spawn_delay)
        {
            playedAnimation = false;
            last_spawn_time = t;
            Spawn(spawn_object, gameObject.transform.forward);

        }
    }

}
