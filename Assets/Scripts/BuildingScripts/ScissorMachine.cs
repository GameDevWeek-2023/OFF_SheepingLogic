using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorMachine : Spawner
{

    public GameObject geschorenes_schaf;

    void OnCollisionEnter(Collision collision)
    {

        GameObject gob = collision.gameObject;
        gob.SetActive(false);

        StartCoroutine(SpawnNext(gob));

    }

    IEnumerator SpawnNext(GameObject gob)
    {

        yield return new WaitForSeconds(spawn_delay);

        gob.SetActive(true);
        Spawn(geschorenes_schaf, gameObject.transform.forward);
        Destroy(gob);
    }

}
