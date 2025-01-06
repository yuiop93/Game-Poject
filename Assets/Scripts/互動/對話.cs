using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class 對話 : MonoBehaviour
{
    private 劇情 劇情1;
    [SerializeField]
    private 劇情_SO 劇情SO;
    private Transform 互動;
    public GameObject[] 攝影機;
    [SerializeField]
    private bool 是否控制 = true;
    public UnityEvent[] unityEvents;
    public UnityEvent EndEvent;
    public bool 可重複播放 = true;
    bool _已撥放 = false;

    // Start is called before the first frame update
    void Start()
    {
        劇情1 = GameObject.Find("UI控制/劇情").GetComponent<劇情>();
    }
    public void 播放劇情()
    {
        if (!可重複播放&&_已撥放)
        {
            Destroy(gameObject);
            return;
        }
        _已撥放 = true;
        劇情1.對話1 = this;
        劇情1.劇情SO = 劇情SO;
        劇情1.攝影機 = 攝影機;
        劇情1.顯示劇情(是否控制);
    }

}
