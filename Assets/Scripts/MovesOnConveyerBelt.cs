using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesOnConveyerBelt : MonoBehaviour
{
     Rigidbody rb;
    Collider col;
    bool vomBandgefallen = false;
    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        col= GetComponent<Collider>();
        col.isTrigger = true;
    }
    void Update()
    {
        float velocity = 1.0f;

        if(!vomBandgefallen)
        {
            //gameObject.transform.position += -transform.up * velocity * Time.deltaTime;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(gameObject.transform.position, -transform.up, out hit, Mathf.Infinity))
            {
                GameObject go = hit.collider.gameObject;


                if (go.tag == "Building")
                {

                    transform.forward = go.transform.forward;

                    Vector3 delta = gameObject.transform.position - go.transform.position;
                    float innerp = Vector3.Dot(delta, go.transform.right);

                    float tolerance = 0.001f;
                    if (innerp < tolerance && innerp > -tolerance)
                    {
                        gameObject.transform.position += go.transform.forward * velocity * Time.deltaTime;
                    }
                    else
                    {
                        gameObject.transform.position += -Mathf.Sign(innerp) * go.transform.right * velocity * Time.deltaTime;
                    }

                }
                else
                {
                    vomBandgefallen = true;
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    col.isTrigger = false;
                    rb.AddForce(2*transform.forward + 1*transform.up, ForceMode.Impulse);
                    StartCoroutine(ZerstoerHeruntergefallenesObjekt());
                }

            }
        }

    }
    IEnumerator ZerstoerHeruntergefallenesObjekt()
    {

        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
