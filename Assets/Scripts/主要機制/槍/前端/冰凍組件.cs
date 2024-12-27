using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 冰凍組件 : MonoBehaviour
{
    public Item 物品;
    [Header("配置參數")]
    public 冰凍組件參數 冰凍組件參數;
    [Header("配置组件")]
    public Transform GunMuzzle;
    private int _傷害;
    private int _射程;
    public int 燃燒傷害;
    public bool 受擊效果 = false;
    [Header("配置特效")]
    public GameObject[] 攻擊特效;
    public GameObject[] 擊中特效;
    public AudioClip[] 卡彈音效;
    public AudioClip[] 射擊音效;
    public AudioClip[] 擊中音效;
    void 生成圖標()
    {
        ItemIconGenerator itemIconGenerator = GameObject.Find("程式/控制").GetComponent<ItemIconGenerator>();
        物品.itemIcon = itemIconGenerator.GenerateIcon(物品);
    }
    public void 步槍(int 射程, int 傷害)
    {
        if (this.GetComponent<可以攻擊>().CanAttack == false || 玩家狀態.能量 < 冰凍組件參數.能量消耗)
        {
            if (卡彈音效[0] != null)
                AudioSource.PlayClipAtPoint(卡彈音效[0], GunMuzzle.position);
            return;
        }
        if (攻擊特效[0] != null)
        {
            GameObject obj = Instantiate(攻擊特效[0], GunMuzzle.position, GunMuzzle.rotation);
            Destroy(obj, 1f);
        }
        if (射擊音效[0] != null)
        {
            AudioSource.PlayClipAtPoint(射擊音效[0], GunMuzzle.position);
        }
        _傷害 = (int)(傷害 * 冰凍組件參數.傷害);
        _射程 = (int)(射程 * 冰凍組件參數.射程);
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _射程))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Mosters")
                    {
                        hit.collider.GetComponent<怪物身體部位>().受傷(_傷害, 受擊效果);
                        hit.collider.GetComponent<怪物身體部位>().冰凍(冰凍組件參數.冰凍效率, 燃燒傷害);
                    }
                    if (hit.collider.tag == "Untagged")
                    {

                    }
                    if (擊中特效 != null)
                    {
                        GameObject obj = Instantiate(擊中特效[0], hit.point, Quaternion.identity);
                        Vector3 cameraPosition = Camera.main.transform.position;

                        // 計算從特效到攝像機的方向
                        Vector3 directionToCamera = cameraPosition - obj.transform.position;

                        // 保持 Y 軸朝向攝像機
                        obj.transform.rotation = Quaternion.LookRotation(Vector3.up, directionToCamera);
                        Destroy(obj, 1f);
                    }
                    if (擊中音效 != null)
                    {
                        AudioSource.PlayClipAtPoint(擊中音效[0], hit.point);
                    }
                }
                else
                {
                    return;
                }
            }

        }

    }
    
    public void 狙擊槍()
    {

    }
    public void 霰彈槍()
    {

    }
}
