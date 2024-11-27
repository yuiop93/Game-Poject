using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 怪物狀態 : MonoBehaviour
{
    [SerializeField]
    private int 怪物血量 = 100;
    // [SerializeField]
    // private int 怪物攻擊力 = 10;
    [SerializeField]
    private string 怪物名稱 = "怪物";

    [SerializeField]
    private GameObject 血條;
    private int 當前血量;
    private bool 是否死亡 = false;
    [SerializeField]
    private GameObject[] 身體部位;
    void Start()
    {
        當前血量 = 怪物血量;
        血條 = Instantiate(血條);
        血條.name = 怪物名稱 + "血條";
        血條.transform.SetParent(GameObject.Find("UI控制/BOSS血條").transform, false);
        血條.GetComponent<BossHealthBar>().ReSetHealth(怪物名稱, 怪物血量);
        血條.GetComponent<BossHealthBar>().SetHealth(當前血量);
        血條.SetActive(true);
        for (int i = 0; i < 身體部位.Length; i++)
        {
            if (身體部位[i] == null) continue;
            身體部位[i].GetComponent<怪物身體部位>().怪物狀態 = this;
        }
    }
    public void 受傷(int 傷害)
    {
        if (是否死亡) return;
        當前血量 -= 傷害;
        if (當前血量 <= 0)
        {
            死亡();
        }
        else
        {
            血條.GetComponent<BossHealthBar>().SetHealth(當前血量);
            this.GetComponent<怪物動畫>().受擊();
        }
    }
    public void 死亡()
    {
        this.GetComponent<怪物動畫>().死亡();
        是否死亡 = true;
        血條.SetActive(false);
        Destroy(gameObject);
    }

}
