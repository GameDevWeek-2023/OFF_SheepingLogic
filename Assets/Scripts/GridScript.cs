using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GridScript : MonoBehaviour
{

    public float resolution;
    public GameObject conveyor_build;
    public GameObject spawner_build;
    public GameObject despawner_build;
    [SerializeField] TMP_Text aufgabenText;

    public int money_initial;
    public TMP_Text money_text;

    int money_amt;
    int aufgabenNummer = 1;
    
    public float sell_fraction;
    public AudioClip building_removed_clip;
    public AudioClip building_complete_clip;

    int researchLevel;
    int spirits;

    #region Aufgabenvariablen
    int[] erforderlicheResourcen = new int [7];
    #endregion

    #region Spielerfortschritt
    int gesammelteSchafe;
    int gesammelteSchwarzeSchafe;
    int gesammelteWolle;
    int gesammelterStrom;
    int gesammelteTeddybären;
    int geopferteTiere;
    int tierVersuche;

    #endregion

    public List<GameObject> buildings;
    public Material building_cursor_mat;
    GameObject building_to_spawn;
    GameObject building_cursor;
    Quaternion build_rotation = Quaternion.identity;

    public GameObject initial_building;

    public bool isOverGUI = false;
    public GameObject fadePanel;

    //Pause Menu
    public GameObject PauseMenu;

    public void togglePauseMenu(bool isPaused)
    {
        PauseMenu.SetActive(isPaused);
        if (isPaused)
        { Time.timeScale = 0; }
        else
        { Time.timeScale = 1; }
    }

    public void changeScene(int sceneIndex)
    {
        StartCoroutine(changeSceneCoroutine(sceneIndex));
    }

    public IEnumerator changeSceneCoroutine(int sceneIndex)
    {
        fadeInOut(is_fade_in: false);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void fadeInOut(bool is_fade_in)
    {
        if (is_fade_in)
        { 
            fadePanel.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(0, 1f, false);
            //fadePanel.GetComponentInParent<Transform>().gameObject.SetActive(false);
        }
        else
        {
            //fadePanel.GetComponentInParent<Transform>().gameObject.SetActive(true);
            fadePanel.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(1, .75f, false);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        togglePauseMenu(false);
        fadeInOut(is_fade_in: true);
        fadePanel.GetComponent<CanvasRenderer>().SetAlpha(0);
        SetBuilding(initial_building);
        money_amt = money_initial;
        NeueAufgabe();
        AktualisiereAufgabenText();

        // Gameobjects of level need to be linked in editor, here the grid position is initialized
        foreach (GameObject b in buildings)
        {
            (int, int) gridpos = ReturnGridCoordinate(b.transform.position);
            b.GetComponent<Building>().grid_position = gridpos;
            b.GetComponent<Building>().destrucible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        // TODO this doesnt belong here
        money_text.text = $"Money: {money_amt} $\n\n" +
            $"Research: {researchLevel}\n\n" +
            $"Spirits: {spirits}";

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                togglePauseMenu(true);
            }
            else if (Time.timeScale == 0)
            {
                togglePauseMenu(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) { 
            build_rotation *= Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Q)) { 
            build_rotation *= Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            building_cursor.transform.rotation = build_rotation;
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
                isOverGUI = true;
            }
            else if(MouseGridposIsFree(out (int, int) grid_pos))
            {
                isOverGUI = false;

                building_cursor.SetActive(false);

                int cost = building_to_spawn.GetComponent<Building>().building_cost;

                if (cost <= money_amt)
                {

                    money_amt -= cost;

                    Vector3 pos = GridPosToWorldspace(grid_pos);
                    Vector3 spawnpos =  pos 
                        + new Vector3(resolution/2.0f, building_to_spawn.GetComponent<Building>().building_height, resolution/2.0f);
        
                    GameObject newObject = Instantiate(building_to_spawn, spawnpos, build_rotation);
                    newObject.GetComponent<Building>().grid_position = grid_pos;

                    newObject.GetComponent<Building>().PlayBuildSound();
                    // newObject.GetComponent<AudioSource>().PlayOneShot();
                    
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
                + new Vector3(resolution/2.0f, building_to_spawn.gameObject.GetComponent<Building>().building_height, resolution/2.0f);
    
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

        if      (delete && ob_delete != null && !ob_delete.GetComponent<Building>().destrucible)
        {
            return false;
        }
        
        else if (delete && ob_delete != null)
        {
            // remove from buildings && delete

            money_amt += (int) Mathf.Ceil(ob_delete.GetComponent<Building>().building_cost * sell_fraction);

            buildings.Remove(ob_delete);
            Object.Destroy(ob_delete);
            
        }

        return true;
        
    }

    (int, int) ReturnGridCoordinate(Vector3 pos)
    {
    
        int x = (int) Mathf.Floor(pos[0] / resolution);
        int z = (int) Mathf.Floor(pos[2] / resolution);

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
        building_cursor = Instantiate(building, Vector3.zero, build_rotation);
        SetMaterial();
        building_cursor.GetComponent<BoxCollider>().enabled = false;
        building_cursor.GetComponent<Building>().enabled = false;
    }

    public void IncrementResearch() { researchLevel++; }

    public void SetMaterial()
    {
        Renderer[] children;
        children = building_cursor.GetComponentsInChildren<MeshRenderer>();
        foreach (Renderer rend in children)
         {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = building_cursor_mat;
            }
            rend.materials = mats;
        }
    }

    public void NeueAufgabe()
    {
        switch(aufgabenNummer)
        {
            case 1:
                erforderlicheResourcen = new int[] {12,0,0,0,0,0,0};
                break;
            default:
                //Spiel gewonnen
                break;
        }
    }

    public void AktualisiereAufgabenText()
    {
        string aufgaben = "Aufgaben:\n";
        if (erforderlicheResourcen[0]!=0)
        {
            aufgaben += (erforderlicheResourcen[0] <= gesammelteSchafe ? "- [x]" : "- [ ]") + $"Liefere {erforderlicheResourcen[0]} Schafe ({gesammelteSchafe}/{erforderlicheResourcen[0]})";
        }
        if (erforderlicheResourcen[1] != 0)
        {
            aufgaben += (erforderlicheResourcen[1] <= gesammelteSchwarzeSchafe ? "- [x]" : "- [ ]") + $"Liefere {erforderlicheResourcen[1]} schwarze Schafe ({gesammelteSchwarzeSchafe}/{erforderlicheResourcen[1]})";
        }
        if (erforderlicheResourcen[2] != 0)
        {
            aufgaben += (erforderlicheResourcen[2] <= gesammelteWolle ? "- [x]" : "- [ ]") + $"Liefere {erforderlicheResourcen[2]} Wolle ({gesammelteWolle}/{erforderlicheResourcen[2]})";
        }
        if (erforderlicheResourcen[3] != 0)
        {
            aufgaben += (erforderlicheResourcen[3] <= gesammelterStrom ? "- [x]" : "- [ ]") + $"Erzeuge {erforderlicheResourcen[3]} Strom ({gesammelterStrom}/{erforderlicheResourcen[3]})";
        }
        if (erforderlicheResourcen[4] != 0)
        {
            aufgaben += (erforderlicheResourcen[4] <= gesammelteTeddybären ? "- [x]" : "- [ ]") + $"Liefer {erforderlicheResourcen[4]} Teddybären ({gesammelteTeddybären}/{erforderlicheResourcen[4]})";
        }
        if (erforderlicheResourcen[5] != 0)
        {
            aufgaben += (erforderlicheResourcen[5] <= geopferteTiere ? "- [x]" : "- [ ]") + $"Opfer {erforderlicheResourcen[5]} Tiere ({geopferteTiere}/{erforderlicheResourcen[5]})";
        }
        if (erforderlicheResourcen[6] != 0)
        {
            aufgaben += (erforderlicheResourcen[6] <= tierVersuche ? "- [x]" : "- [ ]") + $"Experimentiere mit {erforderlicheResourcen[6]} Schafen ({tierVersuche}/{erforderlicheResourcen[6]})";
        }
        aufgabenText.text = aufgaben;
    }
    public void Aufgabenfortschritt()
    {
        int[] array = {gesammelteSchafe,gesammelteSchwarzeSchafe,gesammelteWolle,gesammelterStrom,gesammelteTeddybären,geopferteTiere,tierVersuche};

        if (gesammelteSchafe > erforderlicheResourcen[0] && gesammelteSchwarzeSchafe> erforderlicheResourcen[1] && gesammelteWolle> erforderlicheResourcen[2]&&gesammelterStrom > erforderlicheResourcen[3]&& gesammelteTeddybären > erforderlicheResourcen[4]&& geopferteTiere > erforderlicheResourcen[5]&& tierVersuche > erforderlicheResourcen[6])
        {
            aufgabenNummer++;
            NeueAufgabe();
            AktualisiereAufgabenText();
        }
    }

    public void IncrementMoney()
    {
        money_amt++;
    }
}
