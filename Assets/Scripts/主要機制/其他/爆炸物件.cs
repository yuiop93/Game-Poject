using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 爆炸物件 : MonoBehaviour
{
    public int 爆炸傷害 = 10;
    [SerializeField]
    private float 爆炸時間 = 1;
    [SerializeField]
    private float 爆炸範圍 = 5;
    [SerializeField]
    private AudioClip 爆炸音效;
    private bool 已爆炸 = false;

    void Start()
    {
        AudioSource.PlayClipAtPoint(爆炸音效, transform.position);
        爆炸();
        Destroy(gameObject, 爆炸時間);
    }
    void 爆炸()
    {
        if (已爆炸) return; // 如果已經爆炸過，直接返回
        已爆炸 = true; // 設置已爆炸
        Collider[] colliders = Physics.OverlapSphere(transform.position, 爆炸範圍);
        foreach (Collider nearbyObject in colliders)
        {
            // 檢查物體的 Tag
            if (nearbyObject.gameObject.tag == "Player")
            {
                玩家狀態.血量 -= 10;
            }
            if (nearbyObject.gameObject.tag == "Mosters")
            {
                nearbyObject.gameObject.GetComponent<怪物身體部位>().受傷(爆炸傷害, true);
            }
        }
    }
}
