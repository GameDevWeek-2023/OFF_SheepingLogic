using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShearedSheep : Quest
{

    protected override int GetValue()
    {
        return balloon.GetComponent<Balloon>().SnippedSheep;
    }
}
