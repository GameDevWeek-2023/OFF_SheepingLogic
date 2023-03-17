using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerplant : Spawner
{

    [SerializeField] GameObject ash;

    public int powerLevel = 20;

    public AudioClip building_SFX;
    private AudioSource audio_src;
    private GameObject gridScriptAttach;

    private void Start()
    {
        gridScriptAttach = GameObject.Find("Terrain");
        gridScriptAttach.GetComponent<GridScript>().ChangePower(powerLevel);
    }

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
        Spawn(ash, -transform.right);
        Destroy(gob);
    }

}
