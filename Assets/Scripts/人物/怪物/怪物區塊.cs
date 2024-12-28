using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class 怪物區塊 : MonoBehaviour
{
    public GameObject[] 怪物; // 包含所有怪物的数组
    public GameObject 玩家;  // 目标玩家对象

    void Start()
    {
        // 初始化，将当前怪物区块分配给每个怪物
        foreach (var item in 怪物)
        {
            if (item != null)
            {
                var 怪物狀態 = item.GetComponent<怪物狀態>();
                if (怪物狀態 != null)
                {
                    怪物狀態.怪物區域 = this;
                }
            }
        }
    }

    // 为每个怪物的行为树设置目标玩家
    public void 鎖定敵人()
    {
        if (玩家 == null)
        {
            Debug.LogWarning("玩家未设置，无法锁定敌人！");
            return;
        }

        foreach (var item in 怪物)
        {
            if (item == null) continue;

            // 获取怪物的 BehaviorTree 组件
            var behaviorTree = item.GetComponent<BehaviorTree>();
            if (behaviorTree == null)
            {
                continue;
            }

            // 获取行为树变量并设置目标玩家
            var playerVariable = behaviorTree.GetVariable("Player") as SharedGameObject;
            if (playerVariable != null)
            {
                playerVariable.Value = 玩家; // 设置目标玩家
            }

        }
    }
}
