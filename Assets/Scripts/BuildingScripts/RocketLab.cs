using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLab : Spawner
{

    public GameObject geschorenes_schaf;

    public AudioClip building_SFX;


    void OnTriggerEnter(Collider collision)
    {
        GameObject gob = collision.gameObject;
        gob.SetActive(false);

        StartCoroutine(SpawnNext(gob));
    }


    IEnumerator SpawnNext(GameObject gob)
    {

        yield return new WaitForSeconds(spawn_delay);

        Destroy(gob);

        GameObject rocketsheep = Instantiate(geschorenes_schaf,
            gameObject.transform.position + new Vector3(0.0f, 2.0f, 0.0f),
            Quaternion.identity);

        Destroy(gob);

        rocketsheep.GetComponentInChildren<Rigidbody>().AddForce(transform.up * 10.0f, ForceMode.Impulse);

    }

}
