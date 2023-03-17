using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerplant : Spawner
{

    [SerializeField] GameObject ash;

    public int powerLevel = 20;
    public int fuelLevel = 0;

    public AudioClip building_SFX;
    private AudioSource audio_src;
    private GameObject gridScriptAttach;

    public bool isOperating;

    private void Start()
    {
        gridScriptAttach = GameObject.Find("Terrain");
    }

    void Power()
    {
        gridScriptAttach.GetComponent<GridScript>().ChangePower(-powerLevel);
    }

    private void Update()
    {
        if (fuelLevel <= 0)
        {
            isOperating = false;
        }
        else if (fuelLevel > 0)
        {
            isOperating = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        GameObject gob = collision.gameObject;

        if (!gob.name.Contains("Wolle"))
        {
            return;
        }

        fuelLevel++;
        gob.SetActive(false);

        audio_src = GetComponent<AudioSource>();
        audio_src.PlayOneShot(building_SFX);

        if (gob.tag == "weisseWolle" || gob.tag == "schwarzeWolle")
        {
            // TODO increment power
            StartCoroutine(SpawnNext(gob));
            
        }
        else
        {
            // TODO decrement power
            Destroy(gob);
        }
    }


    IEnumerator SpawnNext(GameObject gob)
    {
        yield return new WaitForSeconds(5f);

        Spawn(ash, transform.right);
        Destroy(gob);
        fuelLevel--;
    }

}
