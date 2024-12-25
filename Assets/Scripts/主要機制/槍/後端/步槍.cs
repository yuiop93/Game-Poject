using UnityEngine;
using System.Collections;
using StarterAssets;
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

    void Awake()
    {
        Aim = GetComponent<Aim>();
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void OnEnable()
    {
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
        }

    }
    private int 能量消耗;
    void Update()
    {
        if (_input.fire && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(Fire());
        }

    }
    private Coroutine fireCoroutine;
    private IEnumerator 後座力()
    {
        while (_input.fire)
        {
            _input.look.y = -後座力度;
            yield return new WaitForSeconds(1 / 攻擊速度);
            _input.look.y = 後座力度 * 0.9f;
            yield return new WaitForSeconds(1 / 攻擊速度);
        }
    }

    private IEnumerator Fire()
    {
        StartCoroutine(後座力());
        while (_input.fire)
        {
            if (有無組件)
            {
                if (能量消耗 <= 玩家狀態.能量)
                {
                    玩家狀態.能量 -= 能量消耗;
                    this.GetComponent<玩家狀態>().開始能量回復();
                    組件攻擊();
                }
            }
            else
            {
                普通攻擊();
            }
            yield return new WaitForSeconds(1 / 攻擊速度);
        }
        _input.look.y = 0;
        fireCoroutine = null;
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
            Destroy(obj1, 0.5f);
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
