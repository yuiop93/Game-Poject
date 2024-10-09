using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class 控制 : MonoBehaviour
{
    public StarterAssetsInputs inputs;
    public void CursorLock()
    {
        inputs.Cursorlock();
    }
    public void CursorUnLock()
    {
        inputs.CursorUnLock();
    }
}
