using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : StateMachine
{
    
    public Camera mainCamera;
    public GameObject[] ships;
    public GameObject[] asteroidPrefabs = new GameObject[3];

    public List<GameObject> asteroids;
    public int level = 1;

    public bool alive = true;
    public int lives = 3;

    public Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ships = GameObject.FindGameObjectsWithTag("Ship");

        SetState(new BeginState(gameManager: this));


    }

    // Update is called once per frame
    void Update()
    {

        UpdateState();
    }

    
    public void spawnAsteroid(Vector3 spawnPosition, int spawnNumber = 1, asteroid.AsteroidSize size = asteroid.AsteroidSize.large)
    {
        int numberSpanwed = 0;

        while (numberSpanwed < spawnNumber)
        {
            GameObject newAsteroid;

            if (size == asteroid.AsteroidSize.small)
            {
                newAsteroid = GameObject.Instantiate(asteroidPrefabs[0]);
            }
            else if (size == asteroid.AsteroidSize.medium)
            {
                newAsteroid = GameObject.Instantiate(asteroidPrefabs[1]);
            }
            else
            {
                newAsteroid = GameObject.Instantiate(asteroidPrefabs[2]);
            }

            newAsteroid.transform.position = spawnPosition;
            numberSpanwed += 1;
            asteroids.Add(newAsteroid);
        }

    }


    public Vector3 calculateSpawnPos()
    {
        //randomly select if we will spawn on x or y axis:
        bool yAxis = Random.Range(0, 2) > 0;

        if (yAxis)
        {
            //Calculates start vecotr in Viewport coordinates
            Vector3 screenPos = new Vector3(Random.Range(0.0f, 1.0f), 1.04f, 20);
            return mainCamera.ViewportToWorldPoint(screenPos);
        }
        else
        {
            Vector3 screenPos = new Vector3(1.04f, Random.Range(0.0f, 1.0f), 20);
            return mainCamera.ViewportToWorldPoint(screenPos);
        }

    }




}

