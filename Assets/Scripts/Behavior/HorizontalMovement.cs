using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HorizontalMovement : Action
{
    public SharedBool isMovingLeft; // 當前是否向左移動
    public SharedFloat moveTime;   // 剩餘移動時間
    public SharedFloat speed;      // 移動速度
    private float timer;
    private Animator animator;
    public override void OnStart()
    {
        timer = moveTime.Value;
        animator = GetComponent<Animator>();
        animator.SetBool("HorizontalMovement", true);
        float animDirection = isMovingLeft.Value ? -1f : 1f;
        animator.SetFloat("MoveDirection", animDirection);
    }

    public override TaskStatus OnUpdate()
    {
        timer -= Time.deltaTime;

        // 根據方向移動
        float direction = isMovingLeft.Value ? -1f : 1f;
        transform.Translate(Vector3.right * direction * speed.Value * Time.deltaTime);

        // 若移動時間結束，返回成功
        if (timer <= 0)
        {
            animator.SetBool("HorizontalMovement", false);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
