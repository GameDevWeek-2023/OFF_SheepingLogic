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
        animator.Play("PlaceObject");
    }

    void OnTriggerEnter(Collider collision)
    {
        GameObject gob = collision.gameObject;
        gob.SetActive(false);

        if (gob.tag == "weißesSchaf" || gob.tag == "schwarzesSchaf")
        {
            audio_src = GetComponent<AudioSource>();
            audio_src.PlayOneShot(building_SFX);
            animator.Play("ReciveObject");
        }
        
        StartCoroutine(SpawnNext(gob));
    
    }


    IEnumerator SpawnNext(GameObject gob)
    {

        yield return new WaitForSeconds(spawn_delay);
        animator.Play("SpawnObject");
        gob.SetActive(true);

        if (gob.tag == "weissesSchaf" || gob.tag == "schwarzesSchaf")
        {
            Spawn(geschorenes_schaf, gob.transform.forward);

            // TODO adjust wool color here
            Spawn(wolle, -transform.right);
        }
        else
        {
            Spawn(gob, gob.transform.forward);
        }
        
        Destroy(gob);
    }

}
