using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddybaerenFabrikScript : Spawner
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject teddybaer;
    int wolle = 0;
    int garn=0;

    private void Start()
    {
        animator.Play("PlaceObject");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.name.Contains("Wolle"))
        {
            wolle++;
            animator.Play("reciveObject");
            if(garn > 0)
            {
                garn--;
                StartCoroutine(SpawnTeddabear(collision.gameObject));
            }
            collision.gameObject.SetActive(false);
            animator.Play("ReciveObject");
        }
        if (collision.name.Contains("Garn"))
        {
            garn++;
            animator.Play("reciveObject");
            if (wolle > 0)
            {
                wolle--;
                StartCoroutine(SpawnTeddabear(collision.gameObject));
            }
            collision.gameObject.SetActive(false);
            animator.Play("ReciveObject");
        }
    }
    IEnumerator SpawnTeddabear(GameObject gob)
    {
        yield return new WaitForSeconds(spawn_delay - 0.15f);
        animator.Play("SpawnObject");
        yield return new WaitForSeconds(0.15f);
        Spawn(teddybaer,gob.transform.forward);

        Destroy(gob);
    }

}
