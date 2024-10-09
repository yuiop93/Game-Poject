using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class 坐下 : MonoBehaviour
{
    public Transform sitpoint;
    public Animator animator;
    bool isSitting = false;

    public void sit()
    {
        this.transform.SetPositionAndRotation(sitpoint.transform.position, sitpoint.transform.rotation);
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<PlayerInput>().enabled = false;
        animator.SetTrigger("Sit");
        Invoke("isSit", 2);

    }
    public void stand()
    {
        Invoke("Standing", .2f);
        sitpoint = null;
        animator.SetTrigger("Stand");
    }
    void isSit()
    {
        isSitting = true;
    }
    
    void Standing()
    {
        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<PlayerInput>().enabled = true;
        
    }
    void Update()
    {
        if (isSitting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stand();
                isSitting = false;
            }
        }
    }
}
