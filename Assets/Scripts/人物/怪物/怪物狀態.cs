using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 怪物狀態 : MonoBehaviour
{
    [SerializeField]
    private GameObject 血條UI;
    [Header("怪物血量")]
    [SerializeField]
    private Image 怪物血量條;
    [SerializeField]
    private int 怪物血量最大值 = 100;
    private int 怪物血量;
    
    private void 怪物血量UI()
    {
        怪物血量條.fillAmount = (float)怪物血量 / 怪物血量最大值;
    }
    public int 怪物血量_當前值
    {
        get
        {
            return 怪物血量;
        }
        set
        {
            怪物血量 = value;
            if (怪物血量 <= 0)
            {
                怪物血量 = 0;
                怪物死亡();
            }
            if (怪物血量 >= 怪物血量最大值)
            {
                怪物血量 = 怪物血量最大值;
            }
        }
    }
    public void 怪物受傷(int 傷害值)
    {
        怪物血量_當前值 -= 傷害值;
    }
    public void 怪物死亡()
    {
        Destroy(gameObject);
    }
    public void 怪物狀態_初始化()
    {
        怪物血量 = 怪物血量最大值;

    }
    
    void Start()
    {
        怪物狀態_初始化();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(怪物血量條 != null)
        怪物血量UI();
    }
}
