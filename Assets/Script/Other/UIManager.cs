using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Slot Skills
    public Image imgEmptySlot1;                                                     //Slot Sparo 1
    public Image imgEmptySlot2;                                                     //Slot Sparo 2
    public TMP_Text TXTAmmo1;                                                       //Munizioni Sparo 1
    public TMP_Text TXTAmmo2;                                                       //Munizioni Sparo 2

    //Equip Slot Sparo
    public void EquipSlot(GameObject gem, Image imgEmptySlot)
    {
        if (gem.name == "FireGem" || gem.name == "FireGem(Clone)")                  //Se la gemma è FireGem
        {
            imgEmptySlot.sprite = Resources.Load<Sprite>("skill_fuoco_attiva");     //Equipaggia FireGem
        }
        if (gem.name == "WaterGem" || gem.name == "WaterGem(Clone)")                //Se la gemma è WaterGem
        {
            imgEmptySlot.sprite = Resources.Load<Sprite>("skill_acqua_attiva");     //Equipaggia WaterGem
        }
    }

    //Svuota uno Slot Sparo
    public void EmptySlot(Image imgEmptySlot)
    {
        imgEmptySlot.sprite = Resources.Load<Sprite>("empty_skill");                //Equipaggia Slot vuoto
    }
}
