using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Si occupa della gestione dei controlli nel gioco*/
public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
