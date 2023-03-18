using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Quest : MonoBehaviour
{

    public GameObject balloon;
    public TMP_Text description;
    public TMP_Text target_ct;
    public GameObject toggle;
    public string description_text;
    public int ct_total;

    public GameObject gridScriptAttach;

    private bool complete = false;
    
    void Start()
    {
        description.text = description_text;
        toggle.GetComponent<Toggle>().isOn = false;
    }


    bool fulfilled ()
    {
        return GetValue() == ct_total;
    }

    void Update()
    {
        target_ct.text = GetValue().ToString() + " / " + ct_total.ToString();
        if (fulfilled() && balloon != null && !complete)
        {
            toggle.GetComponent<Toggle>().isOn = true;
            complete = true;
            StartCoroutine(TriggerAnim());
        }

    }

    IEnumerator TriggerAnim()
    {
        balloon.GetComponent<Balloon>().QuestCompleted = true;
        
        yield return new WaitForSeconds(balloon.GetComponent<Balloon>().FlyTime);

        gridScriptAttach.GetComponent<GridScript>().questsCompleted++;
        gridScriptAttach.GetComponent<GridScript>().delete_building_from_grid(balloon);

    }



    abstract protected int GetValue();
    
}