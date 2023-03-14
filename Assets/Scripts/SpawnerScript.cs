using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : Building
{

    float last_spawn_time;

    public float spawn_interval;
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

        if (t - last_spawn_time > spawn_interval)
        {
            last_spawn_time = t;
            Instantiate(spawn_object, gameObject.transform.position + gameObject.transform.forward, Quaternion.identity);
        }
    }

    public override float GetSpawnHeight()
    {
        return 1.4f;
    }

    public override int GetCost()
    {
        return 10;
    }
}
