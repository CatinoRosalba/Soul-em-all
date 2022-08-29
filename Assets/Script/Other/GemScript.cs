using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GemScript : MonoBehaviour
{
    public bool canDespawn = true;
    public int ammo;
    public int min;
    public int max;

    private void Awake()
    {
        min = 3;
        max = 7;
        ammo = Random.Range(min, max);
    }

    void Start()
    {
        if (canDespawn)
        {
            Destroy(gameObject, 15);
        }
    }
}
