using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<string> savedSlimes = new List<string>();
    List<string> keys = new List<string>();

    public void addCollectable(string name, string type)
    {
        if(type == "Key")
        {
            keys.Add(name);
            Debug.Log(keys);
        } else if(type == "Slime")
        {
            savedSlimes.Add(name);
            Debug.Log(savedSlimes);
        }
    }

    public bool CheckKeys(string key)
    {
        if (keys.Contains(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}