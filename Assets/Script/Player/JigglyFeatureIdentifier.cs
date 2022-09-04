using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JigglyFeatureIdentifier : MonoBehaviour
{
    public PlayerAim aim;
    public JigglyFeatures cooldown;
    public Image jigglyAttack;
    public Image jigglyHook;
    private float maxHookRange;                                                                     //Range massimo del rampino
    private float maxJigglyAttackRange;                                                             //Range massimo dell'attacco di Jiggly

    void Start()
    {
        maxHookRange = 40;
        maxJigglyAttackRange = 20;
    }

    private void Update()
    {
        //Ricarica
        if(aim.jigglyRaycasthit.collider.CompareTag("Enemy") && isInRange(maxJigglyAttackRange) && cooldown.CanJigglyAttack)
        {
            jigglyAttack.enabled = true;
        }
        else
        {
            jigglyAttack.enabled = false;   
        }

        //Rampino
        if (aim.jigglyRaycasthit.collider.gameObject.layer == LayerMask.NameToLayer("GrapplingPoint") && isInRange(maxHookRange) && cooldown.canHook)
        {
            jigglyHook.enabled = true;
        }
        else
        {
            jigglyHook.enabled = false;
        }
    }

    private bool isInRange(float maxRange)
    {
        float distance = Vector3.Distance(aim.jigglyRaycasthit.point, gameObject.transform.position);         //Distanza tra player e punto d'aggrappo
        if (distance <= maxRange)                                                                                       //Se minore del range massimo
        {
            return true;                                                                                                //Puoi aggrapparti
        }
        return false;                                                                                                   //Non puoi aggrapparti
    }
}
