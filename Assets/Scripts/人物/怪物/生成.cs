using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
public class 生成 : MonoBehaviour
{
    [Tooltip("生成的怪物")]
    [SerializeField]
    private GameObject 怪物;
    [Tooltip("生成的位置")]
    [SerializeField]
    private Transform 位置;
    [Tooltip("巡逻点")]
    [SerializeField]
    private GameObject[] 巡逻点;

    void Start()
    {
        //Invoke("生成怪物", 1);
    }
    public void 生成怪物()
{
    GameObject gameObject = Instantiate(怪物, 位置.position, 位置.rotation);
    gameObject.name = 怪物.name;

    // 将巡逻点数组转换为 List<GameObject>
    List<GameObject> 巡逻点列表 = new List<GameObject>(巡逻点);

    // 设置行为树中的变量值
    var behaviorTree = gameObject.GetComponent<BehaviorTree>();
    behaviorTree.SetVariableValue("巡邏", 巡逻点列表);

    // 检查赋值是否成功
    var patrolPoints = behaviorTree.GetVariable("巡邏") as SharedGameObjectList;
    if (patrolPoints == null || patrolPoints.Value == null || patrolPoints.Value.Count == 0)
    {
        Debug.LogError("巡邏变量未正确赋值或巡逻点列表为空！");
    }
}

}
