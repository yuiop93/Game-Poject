using UnityEngine;
using System.Collections;

public class 扣血量陷阱 : MonoBehaviour
{
    public int 扣血量 = 5; // 每秒扣除的血量
    private Coroutine damageCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 開始扣血的 Coroutine
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DeductHealth());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 停止扣血的 Coroutine
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DeductHealth()
    {
        while (true)
        {
            if (玩家狀態.血量 > 0) // 只有當血量大於零時才扣血
            {
                玩家狀態.血量 -= 扣血量; // 扣除血量
                if (玩家狀態.血量 < 0) // 確保血量不會小於零
                {
                    玩家狀態.血量 = 0; // 設置為零
                    // 您可以在這裡觸發死亡邏輯
                }
            }

            yield return new WaitForSeconds(1f); // 每秒扣一次
        }
    }
}
