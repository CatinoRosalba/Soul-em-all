using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public UIManager ui;
    public static int keyCounterInt;
    public static int mateCounterInt;

    private List<string> savedSlimes = new List<string>();
    private List<string> keys = new List<string>();

    private void Start()
    {
        keyCounterInt = 0;
        mateCounterInt = 0;
    }

    public void addCollectable(string name, string type)
    {
        if(type == "Key")
        {
            keys.Add(name);
            keyCounterInt++;
            ui.showCollectable(ui.key, keyCounterInt, ui.keyCounter, ui.totalKey);
        } 
        else if(type == "Slime")
        {
            savedSlimes.Add(name);
            mateCounterInt++;
            ui.showCollectable(ui.mate, mateCounterInt, ui.mateCounter, ui.totalMates);
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