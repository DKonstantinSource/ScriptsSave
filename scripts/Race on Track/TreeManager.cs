using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public Transform player;

    public float spawnDistance = 100f;
    public float minX = -9f;
    public float maxX = 9f;
    public float offsetZ = 10f;
    public float treeSpacing = 5f;
    public int treesPerRow = 5;

    private float lastSpawnZ;
    private Terrain terrain;
    private List<GameObject> spawnedTrees = new List<GameObject>();

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player не привязан!");
            return;
        }

        terrain = Terrain.activeTerrain;  // Получаем Terrain
        lastSpawnZ = player.position.z;
        SpawnTrees();
    }

    void Update()
    {
        if (player == null) return;


        if (player.position.z > lastSpawnZ - spawnDistance + offsetZ)
        {
            lastSpawnZ += offsetZ;
            SpawnTrees();
            RemoveOldTrees();
        }
    }

    void SpawnTrees()
{
    for (float z = lastSpawnZ; z < lastSpawnZ + spawnDistance; z += treeSpacing)
    {
        for (int i = 0; i < treesPerRow; i++)
        {
            float randomX;

            if (Random.value > 0.5f)
            {

                randomX = Random.Range(-50f, -9f);
            }
            else
            {

                randomX = Random.Range(9f, 50f);
            }

            SpawnTree(randomX, z);
        }
    }
}


    void SpawnTree(float x, float z)
    {
        GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

        float y = terrain.SampleHeight(new Vector3(x, 0, z)) + 0.05f;

        Vector3 position = new Vector3(x, y, z);
        GameObject tree = Instantiate(treePrefab, position, Quaternion.identity);
        spawnedTrees.Add(tree);
    }

    void RemoveOldTrees()
    {
        float destroyZ = player.position.z - spawnDistance;

        for (int i = spawnedTrees.Count - 1; i >= 0; i--)
        {
            if (spawnedTrees[i] != null && spawnedTrees[i].transform.position.z < destroyZ)
            {
                Destroy(spawnedTrees[i]);
                spawnedTrees.RemoveAt(i);
            }
        }
    }
}

