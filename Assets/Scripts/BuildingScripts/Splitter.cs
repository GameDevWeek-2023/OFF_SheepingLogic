using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : Spawner
{

    int direction = 1;
    [SerializeField] Animator animator1;
    [SerializeField] Animator animator2;

    private void Start()
    {
        animator2.Play("PlaceObject");
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject gob = collision.gameObject;
        gob.SetActive(false);
        StartCoroutine(SpawnNext(gob));
        animator2.Play("ReciveObject");
    }
    IEnumerator SpawnNext(GameObject gob)
    {
        if (direction == 1)
        {
            animator1.Play("NachLinksDrehen");
        }
        else
        {
            animator1.Play("Nach Rects Drehen");
        }
        yield return new WaitForSeconds(spawn_delay);
        animator2.Play("SpawnObject");
        gob.SetActive(true);
        Spawn(gob, direction * gameObject.transform.right);
        direction *= -1;

        Destroy(gob);
        
    }

}
