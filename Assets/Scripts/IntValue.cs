using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "IntValue")]
public class IntValue : ScriptableObject
{

    public int value;
    public int default_value;

    public void Reset()
    {
        value = default_value;
    }

    public void Increment() { value ++; }

}
