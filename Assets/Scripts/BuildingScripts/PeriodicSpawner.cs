using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PeriodicSpawner : Spawner
{

    float last_spawn_time;
    
    public GameObject spawn_object;
    
    // Start is called before the first frame update
    void Start()
    {
        last_spawn_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        float t = Time.time;

        if (t - last_spawn_time > spawn_delay)
        {
            last_spawn_time = t;

            Spawn(spawn_object, gameObject.transform.forward);

        }
    }
}
