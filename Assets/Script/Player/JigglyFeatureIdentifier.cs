using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglyFeatureIdentifier : MonoBehaviour
{
    public PlayerAim aim;
    private GameObject enemy;
    private GameObject grapplingPoint;
    private Color enemyOldColor;
    private Color grapplingPointOldColor;
    private Color jigglyColor;
    private float maxHookRange;                                                                     //Range massimo del rampino
    private float maxJigglyAttackRange;                                                             //Range massimo dell'attacco di Jiggly

    void Start()
    {
        maxHookRange = 40;
        maxJigglyAttackRange = 20;
        jigglyColor = new Color(13, 255, 0);
    }

    private void Update()
    {
        if(aim.jigglyRaycasthit.collider.CompareTag("Enemy") && isInRange(maxJigglyAttackRange))
        {
            enemy = aim.jigglyRaycasthit.collider.gameObject;
            if(enemy.GetComponentInChildren<SpriteRenderer>().color != jigglyColor)
            {
                enemyOldColor = enemy.GetComponentInChildren<SpriteRenderer>().color;
                Debug.Log(enemyOldColor);
            }
            enemy.GetComponentInChildren<SpriteRenderer>().color = jigglyColor;
        }
        else if(enemy != null)
        {
            enemy.GetComponentInChildren<SpriteRenderer>().color = enemyOldColor;
        }

        /*if (aim.jigglyRaycasthit.collider.gameObject.layer == LayerMask.NameToLayer("GrapplingPoint") && isInRange(maxHookRange))
        {
            grapplingPoint = aim.jigglyRaycasthit.collider.gameObject.transform.parent.gameObject;
            if (grapplingPoint.transform.Find("Sprite").TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite))
            {
                if(sprite.color != jigglyColor)
                {
                    grapplingPointOldColor = sprite.color;
                    Debug.Log(grapplingPointOldColor);
                }
                sprite.color = jigglyColor;
            }
            if(grapplingPoint.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
            {
                if(mesh.material.color != jigglyColor)
                {
                    grapplingPointOldColor = mesh.material.color;
                }
                mesh.material.color = jigglyColor;
            }
        }
        else if (grapplingPoint != null)
        {
            if (grapplingPoint.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite))
            {
                sprite.color = grapplingPointOldColor;
            }
            if (grapplingPoint.TryGetComponent<MeshRenderer>(out MeshRenderer mesh))
            {
                mesh.material.color = grapplingPointOldColor;
            }
        }*/
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
