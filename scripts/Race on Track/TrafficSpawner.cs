    using System.Collections;
    using UnityEngine;

    public class TrafficSpawner : MonoBehaviour
    {
        public GameObject[] trafficPrefabs;
        public Transform player;
        public float spawnRate = 4f;
        public float trafficSpeed = 10f;
        private readonly float[] lanes = { -3f, 0f, 3f };
        private bool isSpawning = true;

        void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player")?.transform;
                if (player == null)
                {
                    Debug.LogError("❌ Ошибка: объект 'Player' не найден! Убедитесь, что у игрока есть тег 'Player'.");
                }
            }

            if (trafficPrefabs.Length == 0)
            {
                Debug.LogError("❌ Ошибка: массив `trafficPrefabs` пуст! Добавьте машины в инспекторе.");
            }

            StartCoroutine(SpawnTraffic());
        }

        IEnumerator SpawnTraffic()
        {
            while (isSpawning)
            {
                yield return new WaitForSeconds(spawnRate);
                SpawnCar();
            }
        }

        void SpawnCar()
        {
            if (player == null)
            {
                Debug.LogError("❌ Ошибка: игрок не найден!");
                return;
            }

            if (trafficPrefabs.Length == 0)
            {
                Debug.LogError("❌ Ошибка: нет машин в массиве `trafficPrefabs`!");
                return;
            }

            int laneIndex = Random.Range(0, lanes.Length);
            Vector3 spawnPosition = new Vector3(lanes[laneIndex], player.position.y, player.position.z + 22f);  // Спавн перед игроком

            GameObject car = Instantiate(trafficPrefabs[Random.Range(0, trafficPrefabs.Length)], spawnPosition, Quaternion.identity);

            Rigidbody rb = car.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = car.AddComponent<Rigidbody>();
            }

            rb.useGravity = false;
            rb.isKinematic = false;
            rb.velocity = Vector3.back * trafficSpeed;

            Debug.Log($"🚗 Машина заспавнена на полосе {lanes[laneIndex]}, позиция: {spawnPosition}");
        }

        public void StopSpawning()
        {
            isSpawning = false;
            Debug.Log("❌ Спавн остановлен!");
        }
    }
