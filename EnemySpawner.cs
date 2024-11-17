using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Standard Enemies")]
    [SerializeField] List<WaveScriptableObject> waveSO;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    private WaveScriptableObject currentWave;

    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;

    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;

    public WaveScriptableObject GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveScriptableObject wave in waveSO)
            {
                currentWave = wave;

                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.identity, transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping);
    }

    void InitializeScreenBounds()
    {
        Camera mainCamera = Camera.main;
        minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
