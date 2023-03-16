using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheeplikeScript : MonoBehaviour
{

    AudioSource audioSource;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;

    private List<AudioClip> clipslist;


    private float last_t;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clipslist = new List<AudioClip>();
        clipslist.Add(clip1);
        clipslist.Add(clip2);
        clipslist.Add(clip3);
        clipslist.Add(clip4);
        clipslist.Add(clip5);

        last_t = Time.time;

    }

    // Update is called once per frame
    void Update()
    {

        float delta_t = 5.0f;

        if (Time.time - last_t > delta_t)
        {
            last_t = Time.time;
            float r = Random.value;
            
            if (r > 0.6)
            {
                last_t += Mathf.Pow(2*r, 2.0f);
                
                AudioClip clip = clipslist[Random.Range(0, 5)];
                audioSource.PlayOneShot(clip);


            }
        }
        



    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbdy = collision.gameObject.GetComponent<Rigidbody>();

        if (rbdy != null)
        {
            //Stop Moving/Translating
            rbdy.velocity = Vector3.zero;

            //Stop rotating
            rbdy.angularVelocity = Vector3.zero;
        }
    }

}
