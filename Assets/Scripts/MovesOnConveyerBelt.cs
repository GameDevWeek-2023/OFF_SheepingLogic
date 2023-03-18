using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesOnConveyerBelt : MonoBehaviour
{
    Rigidbody rb;
    Collider col;
    bool vomBandgefallen = false;

    public bool isAlive = false;
    bool move_freely = false;

    private GameObject gridScriptAttach;

    float ang_velocity = 4.0f;
    float speed_factor = 5.0f; 

    private void Start()
    {
        gridScriptAttach = GameObject.Find("terrain2");
        rb= GetComponent<Rigidbody>();
        col= GetComponent<Collider>();
        rb.isKinematic = true;
        rb.useGravity = false;
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
                    
                    // rotate sheep slowly
                    transform.forward = go.transform.forward;


                    Vector3 delta = gameObject.transform.position - go.transform.position;
                    float innerp = Vector3.Dot(delta, go.transform.right);

                    float tolerance = 0.01f;
                    if (innerp < tolerance && innerp > -tolerance)
                    {
                        gameObject.transform.position += go.transform.forward * velocity * Time.deltaTime;
                    }
                    else
                    {
                        if ((go.transform.right * velocity * Time.deltaTime).magnitude > delta.magnitude)
                        {
                            gameObject.transform.position = go.transform.position;    
                        }
                        else
                        {
                            gameObject.transform.position += -Mathf.Sign(innerp) * go.transform.right * velocity * Time.deltaTime;
                        }
                    }

                }
                else
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.AddForce(2*transform.forward + 1*transform.up, ForceMode.Impulse);
                    
                    vomBandgefallen = true;
                    col.isTrigger = false;
                    if (isAlive)    { StartCoroutine(Fliege()); StartCoroutine(LaufeDavon()); }
                    else            { StartCoroutine(ZerstoerHeruntergefallenesObjekt()); }                
                }

            }

        }
        else if (move_freely)
        {

            Vector3 temp = transform.position;
            temp.y = 0.0f;
            transform.position = temp;

            Vector3 grav = gridScriptAttach.GetComponent<GridScript>().GetGravity(transform.position); 

            gameObject.transform.forward = (gameObject.transform.forward + grav * Time.deltaTime * ang_velocity).normalized;

            gameObject.transform.position += 
                speed_factor * grav * velocity * Time.deltaTime;

        }

    }
    IEnumerator ZerstoerHeruntergefallenesObjekt()
    {

        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    IEnumerator Fliege()
    {
        yield return new WaitForSeconds(0.7f);

        while (transform.position.y > 0.9f)
        {
            yield return new WaitForSeconds(0.2f);
        }

        rb.isKinematic = true;
        rb.useGravity = false;
        move_freely = true;
    }

    IEnumerator LaufeDavon()
    {
        
        // yield return new WaitForSeconds(1);
        
        yield return new WaitForSeconds(20);
        Object.Destroy(gameObject);
    }

    public IEnumerator LerpObjectRotation(GameObject go)
    {
        yield return new WaitForSeconds(.7f);
        transform.forward = Vector3.Lerp(transform.forward, go.transform.forward, Time.deltaTime * 3);
    }

}
