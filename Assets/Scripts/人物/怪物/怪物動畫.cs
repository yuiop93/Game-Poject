using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物動畫 : MonoBehaviour
{
    private Animator _animator; // 動畫控制器
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>(); // 獲取動畫控制器
    }
    public void 被控制()
    {
        _animator.SetBool("Floating", true); // 設置被控制為 true
        _animator.SetFloat("Speed", 0); // 設置速度為 0
    }
    public void 被釋放()
    {
        _animator.SetBool("Floating", false); // 設置被控制為 false
    }
    public void 死亡()
    {
        _animator.SetBool("Dead", true); // 設置死亡為 true
    }
    public void 受擊()
    {
        _animator.SetTrigger("Hit"); // 設置受擊為 true
    }
    public void 攻擊()
    {
        _animator.SetTrigger("Attack"); // 設置攻擊為 true
    }
}
