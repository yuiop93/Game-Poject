using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class 怪物動畫 : MonoBehaviour
{

    public GameObject bulletPrefab; // 子彈Prefab
    public Transform spawnPoint; // 子彈生成位置
    public GameObject _collider; // 碰撞器
    private Animator _animator; // 動畫控制器
    public AudioClip IceAudio;
    public AudioClip FireAudio;
    public AudioClip WalkAudio;

    public Material IceMaterial; // 冰凍材質

    // 保存原始材质的字典
    private Dictionary<Renderer, Material[]> originalMaterials;
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>(); // 獲取動畫控制器
        originalMaterials = new Dictionary<Renderer, Material[]>(); // 初始化字典

    }
    public void 冰凍(bool 是否冰凍)
    {
        if (是否冰凍)
        {
            if (IceAudio != null)
                AudioSource.PlayClipAtPoint(IceAudio, transform.position);
            _animator.speed = 0;
            var behaviorTree = gameObject.GetComponent<BehaviorTree>(); // 獲取行為樹
            BehaviorTree[] behaviorTrees = GetComponents<BehaviorTree>();
            foreach (var bt in behaviorTrees)
            {
                bt.enabled = false;
            }
            ReplaceAllMaterials(); // 替換材質
        }
        else
        {
            _animator.speed = 1;
            var behaviorTree = gameObject.GetComponent<BehaviorTree>(); // 獲取行為樹
            BehaviorTree[] behaviorTrees = GetComponents<BehaviorTree>();
            foreach (var bt in behaviorTrees)
            {
                bt.enabled = true;
            }
            RestoreAllMaterials(); // 恢復材質
        }

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

        var behaviorTree = gameObject.GetComponent<BehaviorTree>(); // 獲取行為樹
        BehaviorTree[] behaviorTrees = GetComponents<BehaviorTree>();
        foreach (var bt in behaviorTrees)
        {
            Destroy(bt); // 刪除行為樹
        }
        var navMeshAgent = gameObject.GetComponent<NavMeshAgent>(); // 獲取導航代理
        Destroy(navMeshAgent); // 刪除導航代理
        _animator.SetBool("Dead", true); // 設置死亡為 true
        Destroy(gameObject, 3); // 3秒後刪除物件
    }
    public void 冰凍死亡()
    {
        Destroy(gameObject,2); // 刪除物件
    }
    public void 禁止移動()
    {
        var behaviorTree = gameObject.GetComponent<BehaviorTree>(); // 獲取行為樹
        BehaviorTree[] behaviorTrees = GetComponents<BehaviorTree>();
        foreach (var bt in behaviorTrees)
        {
            bt.enabled = false;
        }
        var navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        navMeshAgent.enabled = false;
    }
    public void 允許移動()
    {
        var behaviorTree = gameObject.GetComponent<BehaviorTree>(); // 獲取行為樹
        BehaviorTree[] behaviorTrees = GetComponents<BehaviorTree>();
        foreach (var bt in behaviorTrees)
        {
            bt.enabled = true;
        }
        var navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        navMeshAgent.enabled = true;
    }
    public void 受擊()
    {
        _animator.SetTrigger("Hit"); // 設置受擊為 true
        禁止移動();
    }
    public void 受擊結束()
    {
        允許移動();
    }
    public void 攻擊()
    {
        _animator.SetTrigger("Attack"); // 設置攻擊為 true
    }
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
        if (_collider.GetComponent<Collider>().enabled)
            _collider.GetComponent<Collider>().enabled = false; // 禁用碰撞器
    }
    public void ReplaceAllMaterials()
    {
        // 获取当前物体及其所有子物体的 Renderer 组件
        Renderer[] renderers = this.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // 保存原始材质
            if (!this.originalMaterials.ContainsKey(renderer))
            {
                this.originalMaterials[renderer] = renderer.materials;
            }

            // 替换材质
            Material[] IceMaterials = new Material[renderer.materials.Length];
            for (int i = 0; i < IceMaterials.Length; i++)
            {
                IceMaterials[i] = this.IceMaterial;
            }
            renderer.materials = IceMaterials;
        }
    }
    public void RestoreAllMaterials()
    {
        // 获取当前物体及其所有子物体的 Renderer 组件
        Renderer[] renderers = this.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // 检查是否保存了原始材质
            if (this.originalMaterials.ContainsKey(renderer))
            {
                renderer.materials = this.originalMaterials[renderer];
            }
        }
    }
    private void OnFootstep()
    {
        if (WalkAudio != null)
        {
            AudioSource.PlayClipAtPoint(WalkAudio, transform.position, 0.5f);
        }
    }
}
