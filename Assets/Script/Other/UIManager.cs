using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Slot Skills
    public Image imgEmptySlot1;                                                     //Slot Sparo 1
    public Image imgEmptySlot2;                                                     //Slot Sparo 2+
    public Image imgCountDown;                                                      //Immagine Countdown
    public TMP_Text TXTAmmo1;                                                       //Munizioni Sparo 1
    public TMP_Text TXTAmmo2;                                                       //Munizioni Sparo 2
    public TMP_Text TXTCountDown;                                                   //Testo Countdown
    public float countDownTime = 0.5f;                                              //Tempo di partenza del countdown
    public float countDownTimer = 0.0f;                                             //Tempo di fine del countdown
    public bool isCountDown = false;

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

    //Disattiva la skill di attacco di Jiggly e setta il countdown
    public void DisableJigglyAttackSlot()
    {
        //Se il countdown non è attivo resetta
        if (!isCountDown)
        {
            imgCountDown.gameObject.SetActive(true);
            TXTCountDown.gameObject.SetActive(true);
            isCountDown = true;
            imgCountDown.fillAmount = 0.0f;
            countDownTimer = countDownTime;
        }
    }

    public void ApplyCountDown()
    {
        countDownTimer -= Time.deltaTime;                                           //Sottrae al timer il tempo

        if(countDownTimer <= 0.0f)                                                  //Se finisce il tempo
        {
            isCountDown = false;                                                    //Disattiva il countdown
            TXTCountDown.gameObject.SetActive(false);                               //Disattiva il testo
            imgCountDown.gameObject.SetActive(false);                               //Disattiva l'immagine
        }
        else
        {
            TXTCountDown.text = Mathf.RoundToInt(countDownTimer).ToString();        //Setta il testo
            imgCountDown.fillAmount = countDownTimer / countDownTime;               //Setta l'immagine
        }
    }

}
