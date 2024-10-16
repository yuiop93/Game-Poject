using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class 坐下 : MonoBehaviour
{
    public Transform sitpoint;
    private Animator animator;
    private GameObject player;
    public static bool isSitting = false;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = FindObjectOfType<PlayerInput>().GetComponent<Animator>();
    }
    public void sit()
    {
        player.transform.SetPositionAndRotation(sitpoint.transform.position, sitpoint.transform.rotation);
        player.GetComponent<CharacterController>().enabled = false;
        animator.SetTrigger("Sit");
        isSitting = true;
    }
    public void stand()
    {
        Invoke("Standing", .2f);
        animator.SetTrigger("Stand");
    }
    void Standing()
    {
        player.GetComponent<CharacterController>().enabled = true;

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
