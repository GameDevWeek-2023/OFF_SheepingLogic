﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    public (int, int) grid_position;

    GameObject previousBuilding = null;
    GameObject nextBuilding = null; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetParams((int, int) grid_position, GameObject previousBuilding, GameObject nextBuilding)
    {
        this.grid_position = grid_position;
        this.previousBuilding = previousBuilding;
        this.nextBuilding = nextBuilding;

    }   

}
