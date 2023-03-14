﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    public float resolution = 1.0f;

    List<GameObject> buildings;
    public GameObject building_to_spawn;
    public GameObject building_cursor;

    // Start is called before the first frame update
    void Start()
    {
        buildings = new List<GameObject>();
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

    void OnMouseOver()
    {
        
        if(GetMouseGridpos(out (int, int) grid_pos))
        {
            
            Vector3 pos = GridPosToWorldspace(grid_pos);
            
            building_cursor.SetActive(true);

            building_cursor.transform.position = pos;     

            if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)){
                
                building_cursor.SetActive(false);

                GameObject newObject = Instantiate(building_to_spawn, pos, Quaternion.identity);

                buildings.Add(newObject);
            
            }
            
        }
        else
        {
            building_cursor.SetActive(false);
        }
    }

    bool CheckPosIsFree((int, int) pos)
    {
        foreach (GameObject b in buildings)
        {
            if (b.GetComponent<BuildingScript>().grid_position == pos) { return false; }
        }

        return true;

    }

    List<GameObject> ReturnNeighbours((int, int) grid_pos)
    {

        List<GameObject> l = new List<GameObject>();

        foreach (GameObject b in buildings)
        {
            (int, int) b_pos = b.GetComponent<BuildingScript>().grid_position; 

            if (-1 <= b_pos.Item1 - grid_pos.Item1 && b_pos.Item1 - grid_pos.Item1 <= 1
             && -1 <= b_pos.Item1 - grid_pos.Item1 && b_pos.Item1 - grid_pos.Item1 <= 1)
            {
                l.Add(b);
            }
        }

        return l;

    }

    bool GetMouseGridpos(out (int, int) pos)
    {
        
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                pos = ReturnGridCoordinate(hit.point);
                return true;
            }
            else
            {
                pos = hit.collider.gameObject.GetComponent<BuildingScript>().grid_position;
                return false;
            }

        }
        else
        {
            pos = (0, 0);
            return false;
        }
        
    }

    (int, int) ReturnGridCoordinate(Vector3 pos)
    {
    
        int x = (int) (pos[0] / resolution);
        int z = (int) (pos[2] / resolution);

        return (x, z);
    
    }

}
