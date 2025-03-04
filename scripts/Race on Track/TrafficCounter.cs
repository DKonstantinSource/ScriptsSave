using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TrafficCounter : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private Transform player;
    public float threshold = -5f;
    private HashSet<GameObject> passedCars = new HashSet<GameObject>();

    private void Start()
    {

        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {

        GameObject[] trafficCars = GameObject.FindGameObjectsWithTag("TrafficCar");

        foreach (GameObject car in trafficCars)
        {

            if (car.transform.position.z < threshold && !passedCars.Contains(car))
            {

                if (car.transform.position.z < player.position.z)
                {
                    passedCars.Add(car);
                    score++;
                    Debug.Log("Score increased to: " + score);
                    UpdateScoreUI();
                }
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "You Score - " + score;
            Debug.Log("Score displayed: " + score);
        }
    }
}
