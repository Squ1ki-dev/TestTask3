using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private Settings settings;
    [SerializeField] private Obstacles obstacles;

    [SerializeField] private float radius;

    private float _randomRadius => Random.Range(-radius, radius);

    private void Start() => Spawn();

    private void Spawn()
    {
        int countSpawned = 0;
        for (int i = countSpawned; i < settings.countObstacles; i++)
        {
            Instantiate(obstacles);
            SetRandomPosition(obstacles.transform);
        }
    }
    private void SetRandomPosition(Transform objectForSpawn)
    {
        Vector3 center = transform.position;
        objectForSpawn.position = new Vector3(center.x + _randomRadius, center.y, center.z + _randomRadius);
    }
}
