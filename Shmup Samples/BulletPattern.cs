using System;
using UnityEngine;

/// <summary>
/// A specific bullet pattern for enemies to use
/// </summary>
[Serializable]
public class BulletPattern
{
    /// <summary>
    /// Set this name in BulletPattern Lists to differentiate between each item
    /// </summary>
    [SerializeField] public string name = "";
    [SerializeField] private TemplateBulletPattern bulletPattern;
    

    /// <summary>
    /// An additive offset to the base bulletPAttern angle
    /// </summary>
    [SerializeField] private float angleOffset = 0f;
    /// <summary>
    /// An additive offset to the base bulletPattern speed
    /// </summary>
    [SerializeField] private float speedOffset = 0f;
    /// <summary>
    /// Delay before pattern starts
    /// </summary>
    [SerializeField] private float delay = 0f;

    private float spawnTime = 0f;
    private int total = 0;


    public bool IsDone() {
        return total >= bulletPattern.totalBulletsShot;
    }

    public void Reset() {
        spawnTime = 0f;
        total = 0;
    }

    public void Step(Vector3 enemyPosition) {
        if (delay > 0f) {
            delay -= Time.deltaTime;
            if (delay <= 0f) {
                spawnTime -= delay;
                delay = 0f;
            }
        }

        if (delay <= 0f) {
            if (spawnTime >= bulletPattern.frequency && total < bulletPattern.totalBulletsShot) {
                total++;
                spawnTime -= bulletPattern.frequency;

                if (bulletPattern.splitAngles != null && bulletPattern.splitAngles.Length > 1) {
                    for (int i = 0; i < bulletPattern.splitAngles.Length; i++) {
                        float splitAngle = bulletPattern.splitAngles[i];

                        BulletManager.SpawnBullet(
                            bulletPattern.visual,
                            bulletPattern.totalBulletsShot - total,
                            enemyPosition,
                            speedOffset + bulletPattern.speed,
                            splitAngle + angleOffset + bulletPattern.angle + (bulletPattern.angularVelocity * total) + (0.5f * bulletPattern.angularAcceleration * total * total),
                            spawnTime
                        );
                    }
                } else {
                    BulletManager.SpawnBullet(
                        bulletPattern.visual,
                        bulletPattern.totalBulletsShot - total,
                        enemyPosition,
                        speedOffset + bulletPattern.speed,
                        angleOffset + bulletPattern.angle + (bulletPattern.angularVelocity * total) + (0.5f * bulletPattern.angularAcceleration * total * total),
                        spawnTime
                    );
                }

            }

            spawnTime += Time.deltaTime;
        }

    }

}
