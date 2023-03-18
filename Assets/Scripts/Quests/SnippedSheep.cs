using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnippedSheep : Quest
{
    protected override int GetValue()
    {
        return balloon.GetComponent<Balloon>().SnippedSheep;
    }
}
