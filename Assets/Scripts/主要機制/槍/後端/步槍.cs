using UnityEngine;
using System.Collections;
using StarterAssets;
using Cinemachine;
public class 步槍 : MonoBehaviour
{
    public int 射程;
    public int 傷害;
    [Tooltip("發子彈/秒")]
    public float 攻擊速度;
    private StarterAssetsInputs _input;
    public bool 有無組件;
    private Aim Aim;
    public Transform GunMuzzle;
    public GameObject 步槍前端;
    public AudioClip 射擊音效;
    public 槍械音效與特效 射擊特效與音效;
    public 組件確認 組件確認;
    public 槍械_SO 槍械描述;
    [SerializeField] private float 後座力度;
    private 玩家狀態 玩家狀態;

    void Awake()
    {
        玩家狀態 = GetComponent<玩家狀態>();
        Aim = GetComponent<Aim>();
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void OnDisable()
    {
        _input.fire = false;
    }
    private void OnEnable()
    {
        _input.fire = false;
        Aim.fixedDistance = 槍械描述.射程;
        組件確認.確認();
        if (組件確認.組件類型.冰凍 != null)
        {
            有無組件 = true;
            能量消耗 = (int)(組件確認.組件類型.冰凍.冰凍組件參數.能量消耗 * 槍械描述.能量消耗);
            步槍前端.SetActive(false);
        }
        else if (組件確認.組件類型.火焰 != null)
        {
            有無組件 = true;
            步槍前端.SetActive(false);
        }
        else
        {
            有無組件 = false;
            步槍前端.SetActive(true);
            能量消耗 = 0;
        }

    }
    public int 能量消耗;
    void Update()
    {
        if (_input.fire && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(Fire());
        }

    }
    private Coroutine fireCoroutine;

    private IEnumerator Fire()
    {
        while (_input.fire)
        {
            if (能量消耗 <= 玩家狀態.能量) // 確認是否有足夠的能量
            {
                玩家狀態.能量使用中 = true;
                CinemachineImpulseSource impulseSource = GetComponent<CinemachineImpulseSource>();
                impulseSource.GenerateImpulse();

                if (有無組件)
                {
                    玩家狀態.能量使用(能量消耗);  // 使用能量
                    組件攻擊();  // 執行特殊攻擊
                }
                else
                {
                    普通攻擊();  // 執行普通攻擊
                }
            }
            else
            {
                // 如果能量不足，停止射擊並開始能量回復
                玩家狀態.能量使用中 = false;
                玩家狀態.開始能量回復();
                break;
            }

            // 控制射擊頻率
            yield return new WaitForSeconds(1 / 攻擊速度);
        }

        // 結束射擊後重設狀態
        fireCoroutine = null;
        玩家狀態.能量使用中 = false;
        玩家狀態.開始能量回復();
    }
    void 組件攻擊()
    {
        if (組件確認.組件類型.冰凍 != null)
        {
            組件確認.組件類型.冰凍.步槍(槍械描述.射程, 傷害);
        }
        else if (組件確認.組件類型.火焰 != null)
        {
            //組件確認.組件類型.火焰.步槍(射程, 傷害);
        }
    }
    void 普通攻擊()
    {
        if (射擊特效與音效.射擊特效[0] != null)
        {
            GameObject obj1 = Instantiate(射擊特效與音效.射擊特效[0], GunMuzzle.transform.position, GunMuzzle.transform.rotation);
            Destroy(obj1, 0.1f);
        }
        AudioSource.PlayClipAtPoint(射擊音效, GunMuzzle.transform.position);
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 槍械描述.射程))
            {
                if (hit.collider != null)
                {
                    if (射擊特效與音效.擊中特效[0] != null)
                    {
                        GameObject obj = Instantiate(射擊特效與音效.擊中特效[0], hit.point, Quaternion.identity);
                        Destroy(obj, 0.5f);
                    }
                    if (hit.collider.tag == "Mosters")
                    {

                        hit.collider.GetComponent<怪物身體部位>().受傷(傷害, false);
                    }
                }
            }
        }


    }
}
