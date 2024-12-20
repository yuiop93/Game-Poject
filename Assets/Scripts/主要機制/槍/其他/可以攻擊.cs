using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 可以攻擊 : MonoBehaviour
{
    public bool CanAttack = true;
    public void CanShoot(int value)
    {
        if (value == 1)
        {
            CanAttack = true;
        }
        else
        {
            CanAttack = false;
        }
        Debug.Log("CanAttack: " + CanAttack);
    }
}
