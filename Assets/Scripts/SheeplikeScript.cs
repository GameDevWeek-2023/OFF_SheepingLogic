using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheeplikeScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float velocity = 1.0f;
        
        //gameObject.transform.position += -transform.up * velocity * Time.deltaTime;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(gameObject.transform.position, -transform.up, out hit, Mathf.Infinity))
        {

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            
            GameObject go = hit.collider.gameObject;

            if (go.tag == "Building")
            {

                Vector3 delta = gameObject.transform.position - go.transform.position;
                float innerp = Vector3.Dot(delta, go.transform.right);
                Debug.Log(innerp);

                float tolerance = 0.0001f;
                if (innerp < tolerance && innerp > -tolerance)
                {
                    gameObject.transform.position += go.transform.forward * velocity * Time.deltaTime;
                }
                else
                {
                    gameObject.transform.position += - Mathf.Sign(innerp) * go.transform.right * velocity * Time.deltaTime;
                }

            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbdy = collision.gameObject.GetComponent<Rigidbody>();

        //Stop Moving/Translating
        rbdy.velocity = Vector3.zero;

        //Stop rotating
        rbdy.angularVelocity = Vector3.zero;
    }

}
