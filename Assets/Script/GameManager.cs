using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

/*Si occupa della gestione dei controlli nel gioco*/
public class GameManager : MonoBehaviour
{
    //SensibilitÓ
    private static readonly string SensitivityX = "SensX";
    [SerializeField] private CinemachineFreeLook cam;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
    }

    private void Update()
    {
        setSensitivity();
    }

    private void setSensitivity()
    {
        cam.m_XAxis.m_MaxSpeed = PlayerPrefs.GetInt(SensitivityX);
    }
}
