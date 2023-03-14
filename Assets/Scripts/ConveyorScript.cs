using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : Building
{
    public override int GetCost()
    {
        return 1;
    }

    public override float GetSpawnHeight()
    {
        return 0.0f;
    }



}
