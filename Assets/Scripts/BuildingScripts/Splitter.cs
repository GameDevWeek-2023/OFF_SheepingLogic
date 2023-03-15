using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : Spawner
{

    int direction = 1; 

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
        Spawn(gob, direction * gameObject.transform.right);
        direction *= -1;
        Destroy(gob);
        
    }

}
