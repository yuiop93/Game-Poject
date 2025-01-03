using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using BehaviorDesigner.Runtime;

public class 主菜單Player : MonoBehaviour
{
    public AudioClip footstep;
    public GameObject[] 槍械;
    int _槍;
    [SerializeField] private Rig[] rigs;
    private Animator animator;
    bool 瞄準狀態 = false;
    bool 是否坐下;
    void Awake()
    {
        是否坐下 = true;
        animator = GetComponent<Animator>();
        指定槍(1);
        清除瞄準();
    }

    public void 坐下()
    {
        if (是否坐下)
        {
            animator.SetTrigger("StandUp");
            animator.ResetTrigger("Sit");
            是否坐下 = false;
        }
        else
        {
            瞄準(false);
            animator.SetTrigger("Sit");
            animator.ResetTrigger("StandUp");
            是否坐下 = true;
        }
    }


    public void 換槍()
    {
        _槍 += 1;
       
        if (_槍 < 0)
        {
            _槍 = 槍械.Length-1;
        }
        else if (_槍 > 槍械.Length)
        {
            _槍 = 0;
        }
         Debug.Log(_槍);
        指定槍(_槍);
    }
    void 指定槍(int i)
    {
        _槍 = i;
        foreach (GameObject 槍械 in 槍械)
        {
            槍械.SetActive(false);
        }
        switch (_槍)
        {
            case 1:
                槍械[0].SetActive(true);
                break;
            case 2:
                槍械[1].SetActive(true);

                break;
            case 3:
                槍械[2].SetActive(true);

                break;
            default:
                break;
        }
        瞄準(瞄準狀態);
    }
    void 瞄準(bool 瞄準)
    {
        瞄準狀態 = 瞄準;
        清除瞄準();
        if (瞄準)
        {
            switch (_槍)
            {
                case 1:
                    animator.SetLayerWeight(2, 1);
                    break;
                case 2:
                    rigs[0].weight = 1;
                    animator.SetLayerWeight(3, 1);
                    break;
                default:
                    break;
            }
        }
    }
    void 清除瞄準()
    {
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
        foreach (var rig in rigs)
        {
            rig.weight = 0;
        }
    }
    public void 切換瞄準()
    {
        瞄準(!瞄準狀態);
    }
    public void 開始行動()
    {
        if (是否坐下)
        {
            坐下();
        }
        if (瞄準狀態)
        {
            瞄準(false);
        }
        指定槍(0);
        BehaviorTree[] behaviorTrees = GetComponents<BehaviorTree>();
        behaviorTrees[0].enabled = false;
        behaviorTrees[1].enabled = true;
    }
    public void OnFootstep()
    {
        AudioSource.PlayClipAtPoint(footstep, transform.position,2);
    }
}
