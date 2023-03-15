using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GridScript : MonoBehaviour
{

    public float resolution;
    public GameObject conveyor_build;
    public GameObject spawner_build;
    public GameObject despawner_build;

    public int money_initial;
    public TMP_Text money_text;

    int money_amt;
    int researchLevel;
    int spirits;
    
    List<GameObject> buildings;
    GameObject building_to_spawn;
    GameObject building_cursor;
    Quaternion rotation = Quaternion.identity;

    public GameObject initial_building;

    public bool isOverGUI = false;


    // Start is called before the first frame update
    void Start()
    {
        buildings = new List<GameObject>();
        SetBuilding(initial_building);
        money_amt = money_initial;
        
    }

    // Update is called once per frame
    void Update()
    {

        // TODO this doesnt belong here
        money_text.text = $"Money: {money_amt} $\n\n" +
            $"Research: {researchLevel}\n\n" +
            $"Spirits: {spirits}";

        if (Input.GetKeyDown(KeyCode.E)) { 
            rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Q)) { 
            rotation *= Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            building_cursor.transform.rotation = rotation;
        }

        if (Input.GetMouseButtonDown(1))
        {
            MouseGridposIsFree(out (int, int) _, true);
        }

        if (!MouseGridposIsFree(out (int, int) _))
        {
            building_cursor.SetActive(false);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) //might not be ideal, but works
            {
                Debug.Log("UI clicked");
                isOverGUI = true;
            }
            else if(MouseGridposIsFree(out (int, int) grid_pos))
            {
                isOverGUI = false;

                building_cursor.SetActive(false);

                int cost = building_to_spawn.GetComponent<Building>().GetCost(); 

                if (cost <= money_amt)
                {

                    money_amt -= cost;

                    Vector3 pos = GridPosToWorldspace(grid_pos);
                    Vector3 spawnpos =  pos 
                        + new Vector3(resolution/2.0f, building_to_spawn.GetComponent<Building>().GetSpawnHeight(), resolution/2.0f);
        
                    GameObject newObject = Instantiate(building_to_spawn, spawnpos, rotation);
                    newObject.GetComponent<Building>().grid_position = grid_pos;
                    
                    buildings.Add(newObject);
                }

            }
            else
            {
                isOverGUI = false;
            }

        }
    }


    void OnMouseOver()
    {
        
        if(MouseGridposIsFree(out (int, int) grid_pos))
        {
             
            Vector3 pos = GridPosToWorldspace(grid_pos);
 
            building_cursor.SetActive(true);

            building_cursor.transform.position = pos 
                + new Vector3(resolution/2.0f, building_to_spawn.GetComponent<Building>().GetSpawnHeight(), resolution/2.0f);
    
        }
        
        
    }

    (int, int) GetMouseGridPos()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        // layer 3 is the Terrain layer
        int layer_mask = 1 << 3;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, layer_mask))
        {
            return ReturnGridCoordinate(hit.point);
        }

        else return (0, 0);

    }

    bool MouseGridposIsFree(out (int, int) pos, bool delete=false)
    {

        pos = GetMouseGridPos();

        GameObject ob_delete = null;

        foreach (GameObject ob in buildings)
        {
            if (ob.GetComponent<Building>().grid_position == pos)
            {
                if (delete)
                {
                    ob_delete = ob;
                }
                else
                {
                    return false;
                }

            }
            
        }

        if (delete && ob_delete != null)
        {
            // remove from buildings && delete

            buildings.Remove(ob_delete);
            Object.Destroy(ob_delete);
            
        }

        return true;
        
    }

    (int, int) ReturnGridCoordinate(Vector3 pos)
    {
    
        int x = (int) (pos[0] / resolution);
        int z = (int) (pos[2] / resolution);

        return (x, z);
    
    }

    Vector3 GridPosToWorldspace((int, int) grid_pos)
    {
        float x = resolution * ((float) grid_pos.Item1);
        float z = resolution * ((float) grid_pos.Item2);
        return new Vector3(x, 0.0f, z);
    }

    public void SetBuilding(GameObject building)
    {
        Object.Destroy(building_cursor);
        building_to_spawn = building;
        building_cursor = Instantiate(building, Vector3.zero, Quaternion.identity);
        building_cursor.GetComponent<BoxCollider>().enabled = false;
        building_cursor.GetComponent<Building>().enabled = false;
    }

    /*public void SetBuildingConveyor()
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
    }*/
}
