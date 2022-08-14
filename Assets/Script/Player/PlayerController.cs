using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Componenti
    Rigidbody rb;                                                           //Componente RigidBody
    Camera mainCamera;                                                      //GameObject MainCamera
    public GameObject sprite;                                               //Sprite del player
    Animator anim;                                                          //Compenente animazione

    //Movimento
    float movSpeed;                                                         //Velocità movimento
    float ZMovement;                                                        //Input movimento asse Z
    float XMovement;                                                        //Inout movimento asse X
    Vector3 movDirection;                                                   //Direzione di movimento

    //Rotazione
    float inputAngle;                                                       //Angolo della visuale e del personaggio

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = sprite.GetComponent<Animator>();
        mainCamera = Camera.main;
        movSpeed = 75f;
    }

    void Update()
    {
        //Input Movimento
        ZMovement = Input.GetAxisRaw("Vertical");
        XMovement = Input.GetAxisRaw("Horizontal");

        //Angolo rotazione
        inputAngle = Mathf.Atan2(XMovement, ZMovement) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;    //input totale della rotazione del personaggio e della cam
        movDirection = Quaternion.Euler(0f, inputAngle, 0f) * Vector3.forward;                                  //Direzione in cui deve muoversi il giocatore in base alla visuale

        //Animazione camminata
        if (rb.velocity != new Vector3(0f, 0f, 0f))                                             //Se si sta muovendo
        {
            setAnimationState(true);                                                            //Attiva camminata                                                   
        }
        else                                                                                    //Se sta fermo
        {
            setAnimationState(false);                                                           //Disattiva camminata
        }
    }

    private void FixedUpdate()
    {
        //Movimento e Rotazione
        rb.MoveRotation(Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f));          //Ruota l'asse del player secondo l'angolo della cam
        if (XMovement!=0 || ZMovement!=0)
        {
            rb.AddForce(movDirection.normalized * movSpeed);                                    //Movimento
        }
    }

    //Animazione camminata
    void setAnimationState(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }
}
