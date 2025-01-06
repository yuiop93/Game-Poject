using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffectHandler : MonoBehaviour
{
    public GameObject slashEffectPrefab;  // 只需要一個斬擊特效的預置物

    [SerializeField]private Transform[] effectSpawnPoints;
    private void PlaySlashEffect(int i)
    {
        Transform effectSpawnPoint = effectSpawnPoints[i];
        if (slashEffectPrefab != null && effectSpawnPoint != null)
        {
            Instantiate(slashEffectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
        }
    }
}
