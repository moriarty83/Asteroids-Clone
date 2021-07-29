using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class asteroid : MonoBehaviour
    {
        //Makes instance of GameManger script
        private GameManager gameManager;


        //Creates asteroid size enum.
        public enum AsteroidSize { small, medium, large }
        public AsteroidSize size;

        private Vector3 maxVelocity = new Vector3(100, 100, 0);

        //Camera is used to calculate screen-based positions.
        private Camera mainCamera;

        //Explosion animation/game object.
        public GameObject explosion;

        //Used to assign random forces to object once spawned
        private int xForce;
        private int yForce;

        //Needed to apply forces.
        private Rigidbody myRigidbody;


        // Start is called before the first frame update
        void Start()
        {
            //myRigidbody.velocity = Vector3.ClampMagnitude(maxVelocity, 100);
            //Populates components that must be populated at Start.
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            mainCamera = Camera.main;
            myRigidbody = this.gameObject.GetComponent<Rigidbody>();
            addForce();

        }

        // Update is called once per frame
        void Update()
        {
            //Runs each frame to determine if asteroid is moving off screen.
            wrapPosition();
        }


        private void wrapPosition()
        {
            //Gets location to object in ViewPort coordinates:
            //Lower left of visible area is (0,0) and upper right is (1,1)
            //Z value indicates distance from camera.
            Vector3 screenPos = mainCamera.WorldToViewportPoint(this.transform.position);


            /*
             * Here we look for instances where the Viewport position of the object
             * has gone .05 past the visible range in any direction, then move it
             * to just outside the field of view on the opposite side of the screen
             * from which it departed.
            */
            if (screenPos.x < -0.05f)
            {
                Vector3 newPos = new Vector3(1.05f, screenPos.y, screenPos.z);
                newPos = mainCamera.ViewportToWorldPoint(newPos);
                this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
            }
            if (screenPos.x > 1.05f)
            {
                Vector3 newPos = new Vector3(-0.05f, screenPos.y, screenPos.z);
                newPos = mainCamera.ViewportToWorldPoint(newPos);
                this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
            }
            if (screenPos.y < -0.05f)
            {
                Vector3 newPos = new Vector3(screenPos.x, 1.05f, screenPos.z);
                newPos = mainCamera.ViewportToWorldPoint(newPos);
                this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
            }
            if (screenPos.y > 1.05f)
            {
                Vector3 newPos = new Vector3(screenPos.x, -0.05f, screenPos.z);
                newPos = mainCamera.ViewportToWorldPoint(newPos);
                this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
            }

        }

        public void addForce()
        {
            //Gives random values for force to apply to asteroid.
            xForce = Random.Range(-100, 100);
            yForce = Random.Range(-100, 100);

            //Adds force to asteroid based on random values which causes asteroids to move around level.
            myRigidbody.AddForce(xForce, yForce, 0, ForceMode.Force);

            //Applies random torque/rotation to asteroid so it spins around.
            myRigidbody.AddRelativeTorque(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }


        public void explodeMe()
        {
            GameObject boom = GameObject.Instantiate(explosion);
            boom.transform.position = this.transform.position;
            if (size == AsteroidSize.large)
            {
                gameManager.spawnAsteroid(this.transform.position, 2, AsteroidSize.medium);
                gameManager.scorePoints(20);
            }
            if (size == AsteroidSize.medium)
            {
                gameManager.spawnAsteroid(this.transform.position, 2, AsteroidSize.small);
                gameManager.scorePoints(10);
            }
            if (size == AsteroidSize.small)
            {
                gameManager.scorePoints(5);
            }
            Destroy(boom, 1.99f);
            Destroy(this.gameObject);

        }

    }

