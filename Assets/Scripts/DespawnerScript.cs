using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerScript : Building
{

    void OnCollisionEnter(Collision collision)
    {
        Object.Destroy(collision.gameObject);
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
