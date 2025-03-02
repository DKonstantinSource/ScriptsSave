using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public GameObject roadPrefab;
    public Transform player;
    public int roadLength = 22;
    public float roadSegmentSize = 10f;

    private Queue<GameObject> roadSegments = new Queue<GameObject>();
    private float lastZ;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("🚨 Ошибка: объект 'player' не назначен в инспекторе!");
            return;
        }

        lastZ = player.position.z;


        for (int i = 0; i < roadLength; i++)
        {
            SpawnRoadSegment();
        }
    }

    void Update()
    {
        if (player == null) return;


        if (player.position.z > lastZ - (roadLength - 1) * roadSegmentSize)
        {
            SpawnRoadSegment();
            RemoveOldSegment();
        }
    }

    void SpawnRoadSegment()
    {
        Vector3 spawnPos = new Vector3(0, 0, lastZ);
        GameObject roadSegment = Instantiate(roadPrefab, spawnPos, Quaternion.identity);
        roadSegments.Enqueue(roadSegment);
        lastZ += roadSegmentSize;
    }

    void RemoveOldSegment()
    {
        if (roadSegments.Count > roadLength)
        {
            GameObject oldSegment = roadSegments.Dequeue();
            Destroy(oldSegment);
        }
    }
}
