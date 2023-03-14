using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerScript : Building
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Object.Destroy(collision.gameObject);
    }

    public override float GetSpawnHeight()
    {
        return 1.4f;
    }
}
