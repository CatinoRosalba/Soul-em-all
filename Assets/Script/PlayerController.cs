using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    Camera mainCamera;
    [SerializeField] Vector3 offset;    //Offset tra MainCamera e Player
    [SerializeField] float movSpeed;
    float ZMovement;
    float XMovement;
    float XMouse;
    float YMouse;

    // Start is called before the first frame update
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        movSpeed = 35f;

        mainCamera = Camera.main;
        offset = mainCamera.transform.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        ZMovement = Input.GetAxisRaw("Vertical");
        XMovement = Input.GetAxisRaw("Horizontal");

        mainCamera.transform.position = transform.position + offset;
        XMouse = Input.GetAxisRaw("Mouse X");
        YMouse = Input.GetAxisRaw("Mouse Y");
        
    }

    private void FixedUpdate()
    {

        rb.AddForce(new Vector3(XMovement, 0f, ZMovement) * movSpeed);

    }
}
