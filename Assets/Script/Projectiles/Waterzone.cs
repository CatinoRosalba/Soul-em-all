using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterzone : MonoBehaviour
{
    float damage;

    private void Start()
    {
        damage = 0.5f;
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }
}
