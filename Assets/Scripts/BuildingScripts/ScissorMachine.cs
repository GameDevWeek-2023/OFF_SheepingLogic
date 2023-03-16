using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorMachine : Spawner
{

    public GameObject geschorenes_schaf;
    [SerializeField] GameObject wolle;
    [SerializeField] Animator animator;
    public AudioClip building_SFX;
    private AudioSource audio_src;

    private void Start()
    {
        animator.Play("Place Object");
    }
    void OnCollisionEnter(Collision collision)
    {

       

    }
    void OnTriggerEnter(Collider collision)
    {
        GameObject gob = collision.gameObject;
        gob.SetActive(false);

        audio_src = GetComponent<AudioSource>();
        audio_src.PlayOneShot(building_SFX);
        animator.Play("EntersObject");
        StartCoroutine(SpawnNext(gob));
    }


    IEnumerator SpawnNext(GameObject gob)
    {
        yield return new WaitForSeconds(spawn_delay);
        animator.Play("LeafObject");
        gob.SetActive(true);
        Spawn(geschorenes_schaf, gob.transform.forward);
        Spawn(wolle, -transform.right);
        Destroy(gob);
    }

}
