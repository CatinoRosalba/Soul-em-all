using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Slot Skills
    public Image imgEmptySlot1;
    public Image imgEmptySlot2;
    public TMP_Text TXTAmmo1;
    public TMP_Text TXTAmmo2;

    //Equip
    public void EquipSlot(GameObject gem, Image imgEmptySlot)
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

    public void EmptySlot(Image imgEmptySlot)
    {
        imgEmptySlot.sprite = Resources.Load<Sprite>("empty_skill");
    }
}
