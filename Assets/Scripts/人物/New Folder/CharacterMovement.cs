using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0f; // 移动速度
    private Transform target;   // 目标位置
    private bool isMoving = false; // 是否正在移动
    public float stopDistance = 0.1f; // 停止移动的阈值

    // 方法用于启动移动
    public void MoveTo(Transform targetTransform)
    {
        target = targetTransform;
        isMoving = true;
        StartCoroutine(MoveCoroutine());
    }

    // 协程用于处理平滑移动
    private System.Collections.IEnumerator MoveCoroutine()
    {
        this.GetComponent<CharacterController>().enabled = false;
        while (isMoving)
        {
            if (target != null)
            {
                // 计算移动方向
                Vector3 direction = target.position - transform.position;

                // 如果与目标的距离大于停止距离，则继续移动
                if (direction.magnitude > stopDistance)
                {
                    // 插值移动角色到目标位置
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
                else
                {
                    // 到达目标位置，停止移动
                    isMoving = false;
                }
            }
            yield return null;
            
        }
        
        GameObject  parent= GameObject.Find("UI控制/互動");
        if(parent!=null)
        for (int i = parent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
        this.GetComponent<CharacterController>().enabled = true;
    }
}
