using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
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
    public void _死亡()
    {
        Destroy(gameObject); // 刪除物件
    }
    public void 受擊()
    {
        _animator.SetTrigger("Hit"); // 設置受擊為 true
    }
    public void 攻擊()
    {
        _animator.SetTrigger("Attack"); // 設置攻擊為 true
    }
    public GameObject bulletPrefab; // 子彈Prefab
    public Transform spawnPoint; // 子彈生成位置
    public GameObject _collider; // 碰撞器
    
    // 動畫事件觸發的方法
    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation); // 生成子彈
        var behaviorTree = gameObject.GetComponent<BehaviorTree>(); // 獲取行為樹
        BehaviorTree[] behaviorTrees = GetComponents<BehaviorTree>();
        foreach (var bt in behaviorTrees)
        {
            if (bt.Group == 3)
            {
                behaviorTree = bt;
                var target = behaviorTree.GetVariable("Player") as SharedGameObject; // 獲取目標
                bullet.GetComponent<Bullet>().Initialize(target.Value.transform.position); // 初始化子彈
                break;
            }
        }
    }
    public void AttackStart()
    {
        _collider.GetComponent<Collider>().enabled = true; // 啟用碰撞器
    }
    public void AttackEnd()
    {
        if(_collider.GetComponent<Collider>().enabled)
            _collider.GetComponent<Collider>().enabled = false; // 禁用碰撞器
    }
}
