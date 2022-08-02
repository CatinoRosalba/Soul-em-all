using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);
    }
}
