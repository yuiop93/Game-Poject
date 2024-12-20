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
    public 冰凍組件 冰凍組件;
    void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }
    void Update()
    {
        if(_input.fire && fireCoroutine == null)
        fireCoroutine = StartCoroutine(Fire());
    }
    private Coroutine fireCoroutine;
    private IEnumerator Fire()
    {
        while (_input.fire)
        {
            yield return new WaitForSeconds(1 / 攻擊速度);
            if(冰凍組件!=null){
                冰凍組件.步槍(射程,傷害);
            }
        }
        fireCoroutine = null;
    }
}
