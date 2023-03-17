using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinMaschineScript : Spawner
{
    [SerializeField] GameObject garn;
    [SerializeField] Animator animator;

    public AudioClip building_SFX;
    private AudioSource audio_src;

    private void Start()
    {
        animator.Play("PlaceObject");
    }
    void OnCollisionEnter(Collision collision)
    {



    }
    void OnTriggerEnter(Collider collision)
    {
        if((collision.transform.forward == transform.forward || collision.transform.forward == -transform.forward) && collision.name.Contains("Wolle"))
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

        yield return new WaitForSeconds(spawn_delay-0.15f);
        animator.Play("SpawnObject");
        yield return new WaitForSeconds(0.15f);
        gob.SetActive(true);
        Spawn(garn, gob.transform.forward);
        Destroy(gob);
    }
}
