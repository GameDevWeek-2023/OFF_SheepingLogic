using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerplant : Spawner
{

    [SerializeField] GameObject wolle;

    public int powerLevel = 20;

    public AudioClip building_SFX;
    private AudioSource audio_src;

    void OnTriggerEnter(Collider collision)
    {
        GameObject gob = collision.gameObject;
        gob.SetActive(false);

        audio_src = GetComponent<AudioSource>();
        audio_src.PlayOneShot(building_SFX);

        StartCoroutine(SpawnNext(gob));
    }


    IEnumerator SpawnNext(GameObject gob)
    {
        yield return new WaitForSeconds(spawn_delay);

        gob.SetActive(true);
        Spawn(gob, gob.transform.forward);
        Spawn(wolle, -transform.right);
        Destroy(gob);
    }

}
