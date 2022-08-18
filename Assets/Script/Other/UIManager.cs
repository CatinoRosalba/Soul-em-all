using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Slot Skills
    [Header("Immagini Slot vuoti")]
    public Image imgEmptySlot1;                                                     //Slot Sparo 1
    public Image imgEmptySlot2;                                                     //Slot Sparo 2

    [Header("Testi ammo")]
    public TMP_Text TXTAmmo1;                                                       //Munizioni Sparo 1
    public TMP_Text TXTAmmo2;                                                       //Munizioni Sparo 2

    [Header("Immagini filtro countdown")]
    public Image imgCountDownAttack;                                                //Immagine Countdown
    public Image imgCountDownHook;                                                  //Immagine Countdown

    [Header("Coutdown Attacco")]
    public TMP_Text TXTCountDownAttack;                                             //Testo Countdown
    public float countDownTimeAttack = 0.5f;                                        //Tempo di partenza del countdown
    public float countDownTimerAttack = 0.0f;                                       //Tempo di fine del countdown
    public bool isAttackCountDown = false;

    [Header("Countdown Rampino")]
    public TMP_Text TXTCountDownHook;                                               //Testo Countdown
    public float countDownTimeHook = 0.5f;                                          //Tempo di partenza del countdown
    public float countDownTimerHook = 0.0f;                                         //Tempo di fine del countdown
    public bool isHookCountDown = false;


    //Equip Slot Sparo
    public void EquipSlot(GameObject gem, Image imgEmptySlot)
    {
        if (gem.name == "FireGem" || gem.name == "FireGem(Clone)")                  //Se la gemma è FireGem
        {
            imgEmptySlot.sprite = Resources.Load<Sprite>("UI/skill_fuoco_attiva");     //Equipaggia FireGem
        }
        if (gem.name == "WaterGem" || gem.name == "WaterGem(Clone)")                //Se la gemma è WaterGem
        {
            imgEmptySlot.sprite = Resources.Load<Sprite>("UI/skill_acqua_attiva");     //Equipaggia WaterGem
        }
    }

    //Svuota uno Slot Sparo
    public void EmptySlot(Image imgEmptySlot)
    {
        imgEmptySlot.sprite = Resources.Load<Sprite>("UI/empty_skill");                //Equipaggia Slot vuoto
    }

    //Disattiva la skill di attacco di Jiggly e setta il countdown
    public void DisableJigglyAttackSlot()
    {
        //Se il countdown non è attivo resetta
        if (!isAttackCountDown)
        {
            imgCountDownAttack.gameObject.SetActive(true);
            TXTCountDownAttack.gameObject.SetActive(true);
            isAttackCountDown = true;
            imgCountDownAttack.fillAmount = 0.0f;
            countDownTimerAttack = countDownTimeAttack;
        }
    }

    public void ApplyAttackCountDown()
    {
        countDownTimerAttack -= Time.deltaTime;                                           //Sottrae al timer il tempo

        if(countDownTimerAttack <= 0.0f)                                                  //Se finisce il tempo
        {
            isAttackCountDown = false;                                                    //Disattiva il countdown
            TXTCountDownAttack.gameObject.SetActive(false);                               //Disattiva il testo
            imgCountDownAttack.gameObject.SetActive(false);                               //Disattiva l'immagine
        }
        else
        {
            TXTCountDownAttack.text = Mathf.RoundToInt(countDownTimerAttack).ToString();  //Setta il testo
            imgCountDownAttack.fillAmount = countDownTimerAttack / countDownTimeAttack;   //Setta l'immagine
        }
    }

}
