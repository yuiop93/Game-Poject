using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 爆炸物件 : MonoBehaviour
{
    public int 爆炸威力 = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            玩家狀態.血量 -= 爆炸威力;
        }
        if (collision.gameObject.tag == "Monsters")
        {
            collision.gameObject.GetComponent<怪物身體部位>().受傷(爆炸威力,true);
        }
    }
}
