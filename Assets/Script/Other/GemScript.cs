using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GemScript : MonoBehaviour
{
    //Particles gems

    [SerializeField] GameObject effect;                             //Effetto caricato 
    public GameObject effectClone;                                 //Effetto applicato

    //Variabili
    public bool canDespawn;
    public int ammo;
    public int min;
    public int max;

    private void Awake()
    {
        canDespawn = false;
        min = 3;
        max = 7;
        ammo = Random.Range(min, max);
    }

    void Start()
    {
        if (canDespawn)
        {
            Destroy(gameObject, 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            effectClone = Instantiate(effect, gameObject.transform.position, Quaternion.identity);       //Particelle
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(effectClone);                                                                   //Stop particelle
        }
    }
}
