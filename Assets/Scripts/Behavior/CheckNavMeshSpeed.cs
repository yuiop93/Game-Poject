using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class CheckNavMeshSpeed : Action
{
    public SharedFloat currentSpeed;      // 用于存储速度值
    public float speedThreshold = 1.0f;   // 速度阈值，用于判断

    private NavMeshAgent agent;

    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        if (agent == null) return TaskStatus.Failure;

        // 获取当前速度大小
        currentSpeed.Value = agent.velocity.magnitude;

        // 判断是否超过指定速度阈值
        if (currentSpeed.Value > speedThreshold)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
