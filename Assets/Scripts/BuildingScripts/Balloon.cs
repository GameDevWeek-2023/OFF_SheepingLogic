using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : Building
{

    public bool QuestCompleted =false;

    public float FlyTime = 3.0f;
    private float velocity = 5.0f;

    public int WhiteSheep {get; set;}
    public int BlackSheep {get; set;}
    public int SnippedSheep {get; set;}
    
    public int WhiteWool {get; set;}
    public int BlackWool {get; set;}

    public int Milk {get; set;} 
    public int Teddys {get; set;}

    public int Garn {get; set;}

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "weissesSchaf")         WhiteSheep ++;
        if (collision.gameObject.tag == "schwarzesSchaf")       BlackSheep ++;
        if (collision.gameObject.tag == "geschorenesSchaf")     SnippedSheep ++;
        if (collision.gameObject.tag == "weisseWolle")          WhiteWool ++;
        if (collision.gameObject.tag == "schwarzeWolle")        BlackWool ++;
        

        Destroy(collision.gameObject);
        //money.Increment();
    }

    void Update ()
    {
        if (QuestCompleted)
        {
            transform.position += transform.up * Time.deltaTime * velocity;
        }
    }

}

