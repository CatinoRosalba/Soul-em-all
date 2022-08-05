using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Slot Skills
    [SerializeField] public Image imgEmptySlot1;
    [SerializeField] public Image imgEmptySlot2;

    //
    public void EquipSlot(GameObject gem, ref Image imgEmptySlot)
    {
        if (gem.name == "FireGem" || gem.name == "FireGem(Clone)")
        {
            imgEmptySlot.sprite = Resources.Load<Sprite>("skill_fuoco_attiva");
        }
        if (gem.name == "WaterGem" || gem.name == "WaterGem(Clone)")
        {
            imgEmptySlot.sprite = Resources.Load<Sprite>("skill_acqua_attiva");
        }
    }
}
