using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    float resolution = 1.0f;

    public GameObject building_to_spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GridPosToWorldspace((int, int) grid_pos)
    {
        float x = resolution * ((float) grid_pos.Item1);
        float z = resolution * ((float) grid_pos.Item2);
        return new Vector3(x, 0.0f, z);
    }

    void OnMouseDown()
    {
        Vector3 pos = GridPosToWorldspace(ReturnGridCoordinate());

        GameObject newObject = Instantiate(building_to_spawn, pos, Quaternion.identity);

    }

    (int, int) ReturnGridCoordinate()
    {
        
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Vector3 pos = hit.point;
    
            int x = (int) (pos[0] / resolution);
            int z = (int) (pos[2] / resolution);

            Debug.Log((x, z));
            return (x,z);

        }
        else
        {
           return (0,0);
        }
        
    }

}
