using UnityEngine;

public class TrafficCollisionDetector : MonoBehaviour
{
    private TrafficSpawner spawner;

    public void SetSpawner(TrafficSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawner.StopSpawning();
        }
    }
}
