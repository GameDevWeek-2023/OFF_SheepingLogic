using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSheep : Quest
{

    
    protected override int GetValue()
    {
        return balloon.GetComponent<Balloon>().WhiteSheep;
    }
}
