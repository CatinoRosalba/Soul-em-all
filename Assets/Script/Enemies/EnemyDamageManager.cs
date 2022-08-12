using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    Enemy enemy;                                                                            //Riferimento allo script Enemy
    bool canDamage;                                                                         //Permette di creare gli invisibility frame

    Material matDefault;                                                                    //materiale di default
    Material matWhite;                                                                      //material di colore bianco
    float flashTime = .10f;                                                                 //tempo del flash

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        canDamage = true;
        matWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        
        //accesso allo sprite figlio dell'oggetto padre Enemy
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            matDefault = r.material;
        }
    }

    public void TakeDamage(float damage, string type)
    {
        if (canDamage == true)
        {
            if (type == enemy.weak)                                             //Se il nemico è debole
            {
                enemy.health -= damage * 2f;                                    //Fai due volte il danno
            }
            else
            {
                enemy.health -= damage;                                         //Calcola il danno
            }
            StartCoroutine(InvisibilityFrame());
            StartCoroutine(EFlash());                                           //Flash del danno
        }
    }

    //Timer del flash
    IEnumerator EFlash()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material = matWhite;
            yield return new WaitForSeconds(flashTime);
            r.material = matDefault;
        }

    }

    //Timer invisibility frame
    IEnumerator InvisibilityFrame()
    {
        canDamage = false;
        yield return new WaitForSeconds(0.7f);
        canDamage = true;
    }
}
