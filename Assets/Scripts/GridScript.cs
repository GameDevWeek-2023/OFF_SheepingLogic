using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    float resolution = 1.0f;

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
    /*
    void OnMouseDown()
    {

        (int, int) grid_pos = GetMouseGridpos();
        building_cursor.SetActive(false);

        if(CheckPosIsFree(grid_pos))
        {

            Vector3 pos = GridPosToWorldspace(grid_pos);

            GameObject newObject = Instantiate(building_to_spawn, pos, Quaternion.identity);

            buildings.Add(newObject);
        
        }
    
    }
    */

    void OnMouseOver()
    {
        (int, int) grid_pos = GetMouseGridpos();

        if(CheckPosIsFree(grid_pos))
        {
            
            Vector3 pos = GridPosToWorldspace(grid_pos);
            
            building_cursor.SetActive(true);

            building_cursor.transform.position = pos;     

            // if the mouse button is being pressed, additionally place object at position.
            if(Input.GetMouseButton(0)){
                
                //building_cursor.SetActive(false);
            
                

                GameObject newObject = Instantiate(building_to_spawn, pos, Quaternion.identity);

                // List<GameObject> neighbours_list = ReturnNeighbours(grid_pos); 

                // newObject.GetComponent<BuildingScript>().SetParams(grid_pos, );


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

    (int, int) GetMouseGridpos()
    {
        
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Vector3 pos = hit.collider.gameObject.transform.position;
            return ReturnGridCoordinate(pos);

        }
        else
        {
           return (0,0);
        }
        
    }

    (int, int) ReturnGridCoordinate(Vector3 pos)
    {
    
        int x = (int) (pos[0] / resolution);
        int z = (int) (pos[2] / resolution);

        return (x, z);
    
    }

}
