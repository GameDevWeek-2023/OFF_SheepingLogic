using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : Spawner
{


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
        Spawn(gob, gameObject.transform.right);
        Spawn(gob, -gameObject.transform.right);
        Destroy(gob);
        
    }

}
