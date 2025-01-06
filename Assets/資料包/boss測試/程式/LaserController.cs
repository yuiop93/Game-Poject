using UnityEngine;
using System.Collections;
public class LaserController : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject chargeEffectPrefab;
    public Transform laserSpawnPoint;
    public Transform chargeSpawnPoint;
    public float chargeDuration = 1f;
    public float laserDuration = 0.5f;
    private GameObject currentLaser;
    private GameObject currentChargeEffect;
    public void StartCharging()
    {
        currentChargeEffect = Instantiate(chargeEffectPrefab, chargeSpawnPoint.position, chargeSpawnPoint.rotation);
        Destroy(currentChargeEffect, chargeDuration);
    }
    public void FireLaserAtPoint()
    {
        currentLaser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        Destroy(currentLaser, laserDuration);
    }
}
