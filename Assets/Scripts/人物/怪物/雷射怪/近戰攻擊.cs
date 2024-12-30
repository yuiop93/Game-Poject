using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 近戰攻擊 : MonoBehaviour
{
    [SerializeField]
    private int attackDamage = 10; // 攻擊傷害
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<玩家狀態>().受傷(attackDamage,false); // 玩家受傷
            this.GetComponent<Collider>().enabled = false; // 禁用碰撞器
        }
    }
}
