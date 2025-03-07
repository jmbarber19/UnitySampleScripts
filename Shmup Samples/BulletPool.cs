using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Object poolign manager for bullets
/// </summary>
public class BulletPool : MonoBehaviour
{
    /// <summary>
    /// Poolable bullet prefab.
    // Create a bullet with a `PoolableBullet` component, make it a prefab, and link it here
    /// </summary>
    [SerializeField] private GameObject poolableBulletPrefab;
    /// <summary>
    /// GameObject to hold all pooled bullet objects
    /// </summary>
    [SerializeField] private GameObject poolHolder;
    
    private static BulletPool instance;
    private List<PooledBullet> pooledBullets;

    /// <summary>
    /// Return a poolable bullet to the pool and hide it
    /// </summary>
    /// <param name="bullet">The bullet to return to the pool</param>
    public static void Removebullet(PooledBullet bullet) {
        bullet.gameObject.SetActive(false);
        bullet.transform.position = Vector3.zero;
    }

    /// <summary>
    /// Return a pooled and available bullet from the list
    /// will mark retrieved bullet as active now
    /// </summary>
    /// <returns>A pooled bullet</returns>
    public static PooledBullet GetAvailableBullet() {
        PooledBullet newBullet = null;
        for (int i = 0; i < instance.pooledBullets.Count; i++) {
            if (!instance.pooledBullets[i].gameObject.activeSelf) {
                newBullet = instance.pooledBullets[i];
            }
        }
        if (newBullet == null) {
            Debug.LogWarning("TOO MANY BULLETS SPAWNED!");
            GameObject instantiated = Instantiate(instance.poolableBulletPrefab, instance.poolHolder.transform);
            PooledBullet bullet = instantiated.GetComponent<PooledBullet>();
            instance.pooledBullets.Add(bullet);
            return bullet;
        }
        newBullet.gameObject.SetActive(true);
        return newBullet;

    }

    void Start()
    {
        instance = this;
        pooledBullets = new List<PooledBullet>(poolHolder.GetComponentsInChildren<PooledBullet>());
        for (int i = 0; i < pooledBullets.Count; i++) {
            pooledBullets[i].gameObject.SetActive(false);
        }
    }
}
