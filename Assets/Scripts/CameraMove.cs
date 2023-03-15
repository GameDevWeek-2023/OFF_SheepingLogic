using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    Vector3 forw;
    float pan_velocity = 10.0f;
    float scroll_velocity = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // forward in world coordinates:
        // https://forum.unity.com/threads/move-camera-forward-in-world-coordinates-using-local-direction.164717/
        forw = transform.forward;
 
        /*get forward in world coords
        forw = transform.InverseTransformDirection(forw);
        */

        //remove the y component (up down rotation)
        forw.y = 0;
        forw.Normalize();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W)) { transform.position += forw * pan_velocity * Time.deltaTime; }
        if (Input.GetKey(KeyCode.S)) { transform.position -= forw * pan_velocity * Time.deltaTime; }
        if (Input.GetKey(KeyCode.D)) { transform.position += transform.right * pan_velocity * Time.deltaTime; }
        if (Input.GetKey(KeyCode.A)) { transform.position -= transform.right * pan_velocity * Time.deltaTime; }

        // https://stackoverflow.com/questions/40830412/unity3d-move-camera-using-mouse-wheel
        float scroll = Input.GetAxis ("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * scroll_velocity, Space.Self);

    }

}
