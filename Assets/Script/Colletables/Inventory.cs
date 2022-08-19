using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<string> savedSlimes = new List<string>();
    private List<string> keys = new List<string>();

    public void addCollectable(string name, string type)
    {
        if(type == "Key")
        {
            keys.Add(name);
        } else if(type == "Slime")
        {
            savedSlimes.Add(name);
        }
    }

    public bool CheckCollectable(string collectable)
    {
        if (keys.Contains(collectable))
        {
            return true;
        }
        if (savedSlimes.Contains(collectable))
        {
            return true;
        }
        return false;
    }
}