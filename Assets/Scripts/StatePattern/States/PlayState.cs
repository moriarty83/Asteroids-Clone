using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayState : State
{
    public PlayState(GameManager gameManager) : base(gameManager)
    {
    }

    private Text scoreAndLives;
    private float spawnTimer = 0;
    private float totalAsteroids;
    private int totalSpawned = 0;

    public override IEnumerator Enter()
    {
        destroyAllAsteroids();
        totalAsteroids = GameManager.level;
        scoreAndLives = GameObject.Find("ScoreAndLives").GetComponent<Text>();
        positionShips(GameManager.ships, GameManager.mainCamera);
        //spawnAsteroids(GameManager.level);
        yield break;    
    }

    public override IEnumerator UpdateState()
    {
        controlShips();
        
        scoreAndLives.text = "Score: " + GameManager.score + "\n" +
            "Lives: " + GameManager.lives;

        
        if (spawnTimer <= 0 && totalSpawned < totalAsteroids)
        {
            spawnAsteroids();
            totalSpawned += 1;
            spawnTimer = 2;
        }

        spawnTimer -= Time.deltaTime;
        checkEndPlay();
        yield return new WaitForEndOfFrame();
        

    }

    public override IEnumerator Exit()
    {
        yield break;
    }



    public void spawnAsteroids(int spawnNumber = 1, asteroid.AsteroidSize size = asteroid.AsteroidSize.large)
    {
        


        GameObject newAsteroid;

        if (size == asteroid.AsteroidSize.small)
        {
            newAsteroid = GameObject.Instantiate(GameManager.asteroidPrefabs[0]);
        }
        else if (size == asteroid.AsteroidSize.medium)
        {
            newAsteroid = GameObject.Instantiate(GameManager.asteroidPrefabs[1]);
        }
        else
        {
            newAsteroid = GameObject.Instantiate(GameManager.asteroidPrefabs[2]);
        }

        newAsteroid.transform.position = GameManager.calculateSpawnPos();
        GameManager.asteroids.Add(newAsteroid);

        

    }

    public void destroyAllAsteroids()
    {
        for (int i = GameManager.asteroids.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(GameManager.asteroids[i]);
            GameManager.asteroids.Remove(GameManager.asteroids[i]);
        }

        GameManager.asteroids = new List<GameObject>();
        totalSpawned = 0;
    }

    private void checkEndPlay()
    {
        if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 0)
        {
            GameManager.SetState(new WinState(GameManager));
        }
        else if(GameManager.lives == 0)
        {
            GameManager.SetState(new GameOverState(GameManager));
        }
        else if(GameManager.alive == false)
        {
            GameManager.SetState(new ContinueState(GameManager));

        }

    }

    private void controlShips()
    {
        if (GameManager.alive) { 
            for (int i = 0; i < GameManager.ships.Length; i++)
            {
                shipController controller = GameManager.ships[i].GetComponent<shipController>();

                controller.flyShip();
                controller.wrapPosition();

            
            }
        }
    }

    //Positions the 9 Ships in the game thas.
    private void positionShips(GameObject[] ships, Camera mainCamera)
    {
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            ships[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ships[i].SetActive(true);
            Vector3 tempPos = new Vector3(0, 0, 0);
            //Set X by dividing by 3
            if (Mathf.Floor(i / 3) == 0)
            {
                tempPos.x = .5f;
            }
            else if (Mathf.Floor(i / 3) == 1)
            {
                tempPos.x = 1.5f;
            }
            else
            {
                tempPos.x = -0.5f;
            }

            //Set y by modulo by 3
            if (i % 3 == 0)
            {
                tempPos.y = .5f;
            }
            else if (i % 3 == 1)
            {
                tempPos.y = 1.5f;
            }
            else
            {
                tempPos.y = -0.5f;
            }

            //Convert those coordinates from ViewPort to World and position ship there.
            Vector3 newPos = mainCamera.ViewportToWorldPoint(new Vector3(tempPos.x, tempPos.y, 20));
            ships[i].transform.position = newPos;
            ships[i].transform.eulerAngles = new Vector3(-90, 0, 0);
        }
    }
}