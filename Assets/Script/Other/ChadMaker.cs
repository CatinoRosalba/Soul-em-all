using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChadMaker : MonoBehaviour
{
    public Image icon;

    private void OnTriggerEnter(Collider other)
    {
        icon.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
