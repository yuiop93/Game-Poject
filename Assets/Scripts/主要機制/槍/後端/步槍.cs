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
    public GameObject GunMuzzle;
    public GameObject 步槍前端;
    public AudioClip 射擊音效;
    public 組件確認 組件確認;
    void Awake()
    {
        Aim = GetComponent<Aim>();
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void OnEnable()
    {
        Aim.fixedDistance = 射程;
        組件確認.確認();
        if(組件確認.組件類型.冰凍!=null)
        {
            有無組件 = true;
            步槍前端.SetActive(false);
        }
        else if(組件確認.組件類型.火焰!=null)
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
    void Update()
    {
        if (_input.fire && fireCoroutine == null)
            fireCoroutine = StartCoroutine(Fire());
    }
    private Coroutine fireCoroutine;
    private IEnumerator Fire()
    {
        while (_input.fire)
        {
            yield return new WaitForSeconds(1 / 攻擊速度);
            if(有無組件)
            {
                組件攻擊();
            }
            else
            {
                普通攻擊();
            }
        }
        fireCoroutine = null;
    }
    void 組件攻擊(){
        if (組件確認.組件類型.冰凍 != null)
        {
            組件確認.組件類型.冰凍.步槍(射程, 傷害);
        }
        else if (組件確認.組件類型.火焰 != null)
        {
            //組件確認.組件類型.火焰.步槍(射程, 傷害);
        }
    }
    void 普通攻擊()
    {
        AudioSource.PlayClipAtPoint(射擊音效, GunMuzzle.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 射程))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Mosters")
                {
                    hit.collider.GetComponent<怪物身體部位>().受傷(傷害, false);
                }
            }
        }
    }
}
