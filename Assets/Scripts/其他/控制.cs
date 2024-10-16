using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
public class 控制 : MonoBehaviour
{
    public StarterAssetsInputs inputs;
    public PlayerInput player;
    public void CursorLock()
    {
        inputs.Cursorlock();
        player.enabled = true;
    }
    public void CursorUnLock()
    {
        player.enabled = false;
        inputs.CursorUnLock();
    }
}
