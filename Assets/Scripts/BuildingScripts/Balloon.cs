using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : Building
{
    [SerializeField] Animator animator;
    public IntValue money;
    public Counter ct;
    public bool QuestCompleted =false;

    public float FlyTime = 3.0f;
    private float velocity = 5.0f;

    void Start()
    {
        ct = new Counter();
    } 

    private void OnTriggerEnter(Collider collision)
    {
        animator.Play("ReciveObject");
        if (collision.gameObject.tag == "weissesSchaf")         ct.WhiteSheep ++;
        if (collision.gameObject.tag == "schwarzesSchaf")       ct.BlackSheep ++;
        if (collision.gameObject.tag == "geschorenesSchaf")     ct.SnippedSheep ++;
        if (collision.gameObject.tag == "weisseWolle")          ct.WhiteWool ++;
        if (collision.gameObject.tag == "schwarzeWolle")        ct.BlackWool ++;
        

        Destroy(collision.gameObject);
        money.Increment();
    }

    void Update ()
    {
        if (QuestCompleted)
        {
            transform.position += transform.up * Time.deltaTime * velocity;
        }
    }

}

public class Counter
{

    public int WhiteSheep {get; set;}
    public int BlackSheep {get; set;}
    public int SnippedSheep {get; set;}
    
    public int WhiteWool {get; set;}
    public int BlackWool {get; set;}

    public int Milk {get; set;} 
    public int Teddys {get; set;}

    public int Garn {get; set;}

}
