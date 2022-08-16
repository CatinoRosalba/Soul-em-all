using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone_1 : MonoBehaviour
{
    private bool clear;
    private GameObject[] fireClone = new GameObject[4];
    private Vector3[] fireClonePosition = new Vector3[4];
    private GameObject[] waterClone;

    // Start is called before the first frame update
    void Start()
    {
        fireClonePosition[1] = new Vector3(54, 1, 22);
        fireClonePosition[2] = new Vector3(72, 1, 33);
        fireClonePosition[2] = new Vector3(71, 1, 6);
        fireClonePosition[2] = new Vector3(46, 1, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && clear == false)
        {
            Debug.Log("a");
            for (int i = 0; i < 4; i++)
            {
                fireClone[i] = (GameObject)Instantiate(Resources.Load("Fire Enemy"), fireClonePosition[i], Quaternion.identity);
                Debug.Log(fireClone);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DestroyAllEnemies();
        }
    }

    private void DestroyAllEnemies()
    {
        //for(int )
    }
}
