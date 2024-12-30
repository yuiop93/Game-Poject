using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // 子彈移動速度
    private float lifetime = 5f; // 子彈存在時間
    private Vector3 direction; // 子彈的移動方向
    public int damage = 1; // 子彈傷害
    // 初始化子彈的方向
    public void Initialize(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
        Destroy(gameObject, lifetime); // 5秒後自動銷毀
    }

    void Update()
    {
        // 持續移動
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<玩家狀態>().受傷(damage,true); // 玩家受傷
        }
        if (other.CompareTag("Mosters"))
        {
            other.GetComponent<怪物身體部位>().受傷(damage,true); // 怪物受傷
        }
        Destroy(gameObject);
    }
}
