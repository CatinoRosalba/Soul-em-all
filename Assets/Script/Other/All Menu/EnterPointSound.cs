using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * Questa classe serve per attivare/disattivare il pointerEnter (quando il cursore passa sulla finestra dei capitoli)
 * e quindi automaticare attivare/disattivare il suono i capitolo sono attivi/disattivi
 */
public class EnterPointSound : EventTrigger, IPointerEnterHandler
{
    public Button _button = null;
    public Button button { get { if (_button == null) { _button = Get(); } return _button; } }

    private Button Get()
    {
        return GetComponent<Button>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable)
                return;
        base.OnPointerEnter(eventData);

        Debug.Log("On Enter");
    }
}
