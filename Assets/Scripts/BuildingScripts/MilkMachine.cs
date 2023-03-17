using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkMachine : Spawner
{

    [SerializeField] GameObject wolle;
    [SerializeField] Animator animator;

    public AudioClip building_SFX;
    private AudioSource audio_src;

    private void Start()
    {
        animator.Play("PlaceObject");
    }

    void OnTriggerEnter(Collider collision)
    {
        if ((collision.transform.forward == transform.forward || collision.transform.forward == -transform.forward))
        {
            GameObject gob = collision.gameObject;
            gob.SetActive(false);

            audio_src = GetComponent<AudioSource>();
            audio_src.PlayOneShot(building_SFX);
            animator.Play("ReciveObject");

            StartCoroutine(SpawnNext(gob));
        }
    }


    IEnumerator SpawnNext(GameObject gob)
    {

        yield return new WaitForSeconds(spawn_delay);
        animator.Play("SpawnObject");
        gob.SetActive(true);
        Spawn(gob, gob.transform.forward);

        // Todo schwarze Wolle
        if (gob.tag == "weissesSchaf" || gob.tag == "schwarzesSchaf")   Spawn(wolle, -transform.right);
        
        Destroy(gob);

    }

}
