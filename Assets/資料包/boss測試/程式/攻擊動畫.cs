using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class 特效生成
{
    public GameObject 特效預置物;
    public Transform 特效生成點;
    public float 特效持續時間;
    
}
public class 攻擊動畫 : MonoBehaviour
{
    public List<特效生成> 特效生成點;
    public void 播放特效(int i)
    {
        if (特效生成點[i].特效預置物 != null && 特效生成點[i].特效生成點 != null)
        {
            GameObject 特效物件 = GameObject.Instantiate(特效生成點[i].特效預置物, 特效生成點[i].特效生成點.position, 特效生成點[i].特效生成點.rotation);
            GameObject.Destroy(特效物件, 特效生成點[i].特效持續時間);
        }
    }
}
