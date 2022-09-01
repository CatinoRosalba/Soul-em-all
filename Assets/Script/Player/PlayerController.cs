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
    float movAerialSpeed;
    float ZMovement;                                                        //Input movimento asse Z
    float XMovement;                                                        //Inout movimento asse X
    Vector3 movDirection;                                                   //Direzione di movimento

    //Se tocca il terreno
    bool isGrounded;

    //Rotazione
    float inputAngle;                                                       //Angolo della visuale e del personaggio

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = sprite.GetComponent<Animator>();
        mainCamera = Camera.main;
        movSpeed = 75f;
        movAerialSpeed = 6.5f;
    }

    void Update()
    {
        //Input Movimento
        ZMovement = Input.GetAxisRaw("Vertical");
        XMovement = Input.GetAxisRaw("Horizontal");

        //Angolo rotazione
        inputAngle = Mathf.Atan2(XMovement, ZMovement) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;    //input totale della rotazione del personaggio e della cam
        movDirection = Quaternion.Euler(0f, inputAngle, 0f) * Vector3.forward;                                  //Direzione in cui deve muoversi il giocatore in base alla visuale

        //Vedo se il giocatore tocca il terreno
        CheckIsGrounded();

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
        if ((XMovement!=0 || ZMovement!=0) && isGrounded == true)
        {
            rb.AddForce(movDirection.normalized * movSpeed);                                    //Movimento
        } else if ((XMovement != 0 || ZMovement != 0) && isGrounded == false)
        {
            rb.AddForce(movDirection.normalized * movAerialSpeed);                              //Movimento
        }
    }

    //Fixa il bug degli spigoli che si blocca, mettere il tag "Cliff" alle zone con questo problema
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cliff") && !isGrounded)
        {
            rb.AddForce(new Vector3(0, 1, 0) * 40);
        }
    }

    //Animazione camminata
    void setAnimationState(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }

    //Controllo se tocca terra
    private void CheckIsGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
