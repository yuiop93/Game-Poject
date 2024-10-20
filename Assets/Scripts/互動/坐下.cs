using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;


public class 坐下 : MonoBehaviour
{
    public Transform sitpoint;
    private Animator animator;
    private GameObject player;
    private StarterAssetsInputs inputs;
    public static bool isSitting = false;
    private bool sittingdown = false;

    void Start()
    {
        player = GameObject.Find("Player");
        inputs = player.GetComponent<StarterAssetsInputs>();
        animator = FindObjectOfType<PlayerInput>().GetComponent<Animator>();
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        sittingdown = false;
    }
    public void sit()
    {
        isSitting = true;
        sittingdown = true;
        StartCoroutine(WaitAndPrint(2f));
        inputs.move = Vector2.zero;
        player.transform.SetPositionAndRotation(sitpoint.transform.position, sitpoint.transform.rotation);
        player.GetComponent<CharacterController>().enabled = false;
        animator.SetTrigger("Sit"); 
    }
    public void stand()
    {
        if (sittingdown)
        {
            return;
        }
        else
        {
            Invoke("Standing", .2f);
            animator.SetTrigger("Stand");
        }

    }
    void Standing()
    {
        inputs.jump = false;
        player.GetComponent<CharacterController>().enabled = true;
        isSitting = false;
    }
    void Update()
    {
        if (isSitting&&!sittingdown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stand();
            }
        }

    }
}
