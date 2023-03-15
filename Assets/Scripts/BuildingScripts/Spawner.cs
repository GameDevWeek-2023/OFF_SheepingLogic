using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : Building
{


    public float spawn_height; 
    public float spawn_delay;
    
    public void Spawn(GameObject spawn_object, Vector3 dir_normed)
    {

        Instantiate(spawn_object,
                gameObject.transform.position + dir_normed * 2 + new Vector3(0.0f, spawn_height, 0.0f),
                Quaternion.identity);
                
    }

}
