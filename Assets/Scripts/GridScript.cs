using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{

    public float resolution;
    public GameObject conveyor_build;
    public GameObject spawner_build;
    public GameObject despawner_build;

    List<GameObject> buildings;
    GameObject building_to_spawn;
    GameObject building_cursor;
    Quaternion rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        buildings = new List<GameObject>();
        SetBuildingConveyor();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E)) { 
            rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Q)) { 
            rotation *= Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            building_cursor.transform.rotation = rotation;
        }

        if(Input.GetMouseButtonDown(0))
        {
            (int, int) grid_position = ReturnGridCoordinate(building_cursor.transform.position);
            building_cursor.SetActive(false);

            // here we discard the position of the mouse in favour of the cursor position.
            if(MouseGridposIsFree(out (int, int) _))
            {
                
                Vector3 pos = GridPosToWorldspace(grid_position);
                Vector3 spawnpos =  pos 
                    + new Vector3(0.0f, building_to_spawn.GetComponent<Building>().GetSpawnHeight(), resolution/2.0f);
    
                GameObject newObject = Instantiate(building_to_spawn, spawnpos, rotation);
                newObject.GetComponent<Building>().grid_position = grid_position;
                
                buildings.Add(newObject);
            
            }
        }
    }

    Vector3 GridPosToWorldspace((int, int) grid_pos)
    {
        float x = resolution * ((float) grid_pos.Item1);
        float z = resolution * ((float) grid_pos.Item2);
        return new Vector3(x, 0.0f, z);
    }

    void OnMouseOver()
    {
        
        if(MouseGridposIsFree(out (int, int) grid_pos))
        {
            
            Vector3 pos = GridPosToWorldspace(grid_pos);
            
            building_cursor.SetActive(true);

            building_cursor.transform.position = pos 
                + new Vector3(0.0f, building_to_spawn.GetComponent<Building>().GetSpawnHeight(), resolution/2.0f);
    
            
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
            if (b.GetComponent<Building>().grid_position == pos) { return false; }
        }

        return true;

    }

    List<GameObject> ReturnNeighbours((int, int) grid_pos)
    {

        List<GameObject> l = new List<GameObject>();

        foreach (GameObject b in buildings)
        {
            (int, int) b_pos = b.GetComponent<Building>().grid_position; 

            if (-1 <= b_pos.Item1 - grid_pos.Item1 && b_pos.Item1 - grid_pos.Item1 <= 1
             && -1 <= b_pos.Item1 - grid_pos.Item1 && b_pos.Item1 - grid_pos.Item1 <= 1)
            {
                l.Add(b);
            }
        }

        return l;

    }

    bool MouseGridposIsFree(out (int, int) pos)
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
                if (hit.collider.gameObject.GetComponent<Building>() == null)
                {
                    pos = (0,0);
                    
                }
                else
                {
                    pos = hit.collider.gameObject.GetComponent<Building>().grid_position;
                }
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

    public void SetBuildingConveyor()
    {
        building_to_spawn = conveyor_build;
        building_cursor = Instantiate(conveyor_build, Vector3.zero, Quaternion.identity);
        building_cursor.GetComponent<BoxCollider>().enabled = false;
    }

    public void SetBuildingSpawner()
    {
        building_to_spawn = spawner_build;
        building_cursor = Instantiate(spawner_build, Vector3.zero, Quaternion.identity);
        building_cursor.GetComponent<BoxCollider>().enabled = false;
    }    
    
    public void SetBuildingDespawner()
    {
        building_to_spawn = despawner_build;
        building_cursor = Instantiate(despawner_build, Vector3.zero, Quaternion.identity);
        building_cursor.GetComponent<BoxCollider>().enabled = false;
    }
}
