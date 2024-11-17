using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private WaveScriptableObject waveSO;
    List<Transform> waypoints;
    private int waypointIndex = 0;

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveSO = enemySpawner.GetCurrentWave();
        waypoints = waveSO.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        
    }

    void FollowPath()
    {
        if(waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveSO.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

            if(transform.position == targetPosition )
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
