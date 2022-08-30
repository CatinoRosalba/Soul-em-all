using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] LevelChanger transition;
    [SerializeField] string nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Passa al prossimo livello
            transition.FadeAndChangeToLevel(nextLevel);

            Debug.Log("fadeOut");
        }
    }
}
