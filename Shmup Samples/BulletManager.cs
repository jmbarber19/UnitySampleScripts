using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    /// <summary>
    /// The asset path of where your bullet prefabs are stored. Each bullet needs a `Bullet` component
    /// </summary>
    [SerializeField] private string prefabBulletsPath = "Assets\\Prefabs\\Bullets";
    
    private Dictionary<BulletVisual, PooledBullet> bulletPrefabs = new Dictionary<BulletVisual, PooledBullet>();
    private static BulletManager instance;


    /// <summary>
    /// Spawns a bullet with the given properties
    /// </summary>
    /// <param name="bulletVisual">The visual look of the bullet</param>
    /// <param name="order">Sprite sorting order</param>
    /// <param name="position">Starting point of the bullet</param>
    /// <param name="speed">Speed of the bullet</param>
    /// <param name="angle">Bearing angle of the bullet</param>
    /// <param name="timeSkip">How many seconds to update this bullet by initially (Set this value if trying to spawn bullets at time based intervals)</param>
    public static void SpawnBullet(
        BulletVisual bulletVisual,
        int order,
        Vector3 position,
        float speed,
        float angle,
        float timeSkip
    ) {
        PooledBullet prefab;
        instance.bulletPrefabs.TryGetValue(bulletVisual, out prefab);

        if (prefab == null) {
            Debug.LogError($"Could not find bulletVisual \"{bulletVisual}\" in Bulletmanger");
            return;
        }

        PooledBullet bullet = BulletPool.GetAvailableBullet();

        bullet.Initialize(prefab, order, position, speed, angle, timeSkip);
    }


    private void FillBulletDictionary() {
        if (bulletPrefabs.Count != 0) return;

        var loadedAssets = AssetDatabase.FindAssets("", new string[] {prefabBulletsPath});
        

        for (int i = 0; i < loadedAssets.Length; i++) {
            string foundPath = AssetDatabase.GUIDToAssetPath(loadedAssets[i]);
            PooledBullet bullet = AssetDatabase.LoadAssetAtPath<PooledBullet>(foundPath);
            bullet.AssignComponents();
            if (bullet) {
                try {
                    bulletPrefabs.Add(bullet.visual, bullet);
                } catch (ArgumentException exception) {
                    Debug.LogError(exception);
                    Debug.LogError($"!! Error adding {bullet.visual} to prefab list; key alerady added.");
                }
            }
        }
    }

    private void Start() {
        instance = this;
        FillBulletDictionary();
    }
}
