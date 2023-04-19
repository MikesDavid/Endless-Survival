using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool canSpawnEnemy1, canSpawnEnemy2, canSpawnEnemy3;
    [SerializeField] private GameObject enemy1Prefab, enemy2Prefab, enemy3Prefab;

    public float enemy1Interval = 3.5f;

    public float enemy2Interval = 5f;

    public float enemy3Interval = 1f;
    private void Start()
    {
        if(canSpawnEnemy1)
            StartCoroutine(SpawnEnemy1(enemy1Interval, enemy1Prefab));
        if (canSpawnEnemy2)
            StartCoroutine(SpawnEnemy1(enemy2Interval, enemy2Prefab));
        if (canSpawnEnemy3)
            StartCoroutine(SpawnEnemy1(enemy3Interval, enemy3Prefab));
    }

    //private void FixedUpdate()
    //{
    //    LevelManager.OnSceneChanged += LevelManager_OnSceneChanged;
    //}
    //private void OnDestroy()
    //{
    //    LevelManager.OnSceneChanged -= LevelManager_OnSceneChanged;

    //}
    //private void LevelManager_OnSceneChanged(GameScene scene)
    //{
    //    if(scene == GameScene.Defeat || scene == GameScene.Wictory)
    //        StopAllCoroutines();

    //}

    public IEnumerator SpawnEnemy1(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-46f,46),0 ,Random.Range(-46f,46f)), Quaternion.identity);
        StartCoroutine(SpawnEnemy1(interval, enemy));
    }
    public IEnumerator SpawnEnemy2(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-46f, 46), 0, Random.Range(-46f, 46f)), Quaternion.identity);
        StartCoroutine(SpawnEnemy2(interval, enemy));
    }
    public IEnumerator SpawnEnemy3(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-46f, 46), 0, Random.Range(-46f, 46f)), Quaternion.identity);
        StartCoroutine(SpawnEnemy3(interval, enemy));
    }
}
