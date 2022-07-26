using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] GameObject Ammo1;
    GameObject Ammo2;

    bool isshooting = false;

    // Update is called once per frame

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            isshooting = true;
        }

    }

    void FixedUpdate()
    {

        if (isshooting)
        {

            //Fuoco primario
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(Fire(Ammo1));
            }

            //Fuoco secondario
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartCoroutine(Fire(Ammo2));
            }

        }

    }

    IEnumerator Fire(GameObject ammo)
    {

        isshooting = false;
        GameObject clone = Instantiate(ammo, gameObject.transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 1f) * 20f;
        yield return new WaitForSeconds(4);
        Destroy(clone);

    }

}
