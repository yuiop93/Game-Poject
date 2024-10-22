using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
public class 控制 : MonoBehaviour
{
    private StarterAssetsInputs inputs;
    private PlayerInput player;
    [SerializeField]
    private GameObject 互動;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInput>();
        inputs = player.GetComponent<StarterAssetsInputs>();
        互動 = GameObject.Find("UI控制/互動");
    }
    public void CursorLock()
    {
        inputs.Cursorlock();
        互動.SetActive(true);
        player.enabled = true;
    }
    public void CursorUnLock()
    {
        player.enabled = false;
        互動.SetActive(false);
        inputs.CursorUnLock();
    }
}
