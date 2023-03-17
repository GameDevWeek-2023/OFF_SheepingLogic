using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetText : MonoBehaviour
{

    private TMP_Text text;
    public IntValue thing;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = thing.value.ToString();
        

    }

    void Update()
    {
        text.text = thing.value.ToString();
    }

}
