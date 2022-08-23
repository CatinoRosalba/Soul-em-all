using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent onTrigger;
    bool isTriggered = true;

    private void Awake()
    {
        if(onTrigger == null)
        {
            onTrigger = new UnityEvent();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        onTrigger.Invoke();

        if (isTriggered)
        {
            Destroy(gameObject);
        }
    }


}
