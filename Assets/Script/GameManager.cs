using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

/*Si occupa della gestione dei controlli nel gioco*/
public class GameManager : MonoBehaviour
{
    //Sensibilità
    private static readonly string SensitivityX = "SensX";
    [SerializeField] private FreeLookAxisDriver cam;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        SetSensitivity();
    }

    private void SetSensitivity()
    {
        cam.xAxis.multiplier = PlayerPrefs.GetFloat(SensitivityX);
    }
}
