using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{

    public (int, int) grid_position;

    public abstract float GetSpawnHeight();

}
