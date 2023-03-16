using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : Spawner
{
    [SerializeField] Animator animator;
    int direction = 1;
    private void Start()
    {
        animator.Play("PlaceObject1");
    }
    void OnCollisionEnter(Collision collision)
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        GameObject gob = collision.gameObject;
        gob.SetActive(false);
        StartCoroutine(SpawnNext(gob));
    }
    IEnumerator SpawnNext(GameObject gob)
    {

        yield return new WaitForSeconds(spawn_delay);
        animator.Play("SpawnObjects");
        gob.SetActive(true);
        Spawn(gob, direction * gameObject.transform.right);
        direction *= -1;
        Destroy(gob);
        
    }

}
