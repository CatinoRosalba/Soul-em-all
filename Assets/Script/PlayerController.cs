using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Componenti
    Rigidbody rb;
    Camera mainCamera;
    [SerializeField] GameObject sprite;

    //Movimento
    float movSpeed = 35f;
    float ZMovement;
    float XMovement;
    Vector3 movDirection;

    //Rotazione
    float inputAngle;
    float turnSmoothAngle = 0.1f;
    float turnSmoothVelocity;
    float angle;

    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        mainCamera = Camera.main;

    }

    void Update()
    {
        //Input Movimento
        ZMovement = Input.GetAxisRaw("Vertical");
        XMovement = Input.GetAxisRaw("Horizontal");

        //Angolo rotazione
        inputAngle = Mathf.Atan2(XMovement, ZMovement) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;    //input totale della rotazione del personaggio e della cam
        angle = Mathf.SmoothDampAngle(gameObject.transform.eulerAngles.y, inputAngle, ref turnSmoothVelocity, turnSmoothAngle); //Angolo della rotazione
        movDirection = Quaternion.Euler(0f, inputAngle, 0f) * Vector3.forward;   //Direzione in cui deve muoversi il giocatore considerando l'angolo

        //Billboarding
        sprite.transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);



    }

    private void FixedUpdate()
    {

        //Movimento e Rotazione
        rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));   //Ruota secondo l'angolo fluido e cambia l'asse di movimento
        if (XMovement!=0 || ZMovement!=0)
        {
            rb.AddForce(movDirection.normalized * movSpeed);
        }
        
    }
}
