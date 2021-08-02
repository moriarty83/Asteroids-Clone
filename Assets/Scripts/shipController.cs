using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour
{
    private Camera mainCamera;
    public Renderer myRenderer;
    public Rigidbody myRigidbody;

    public GameObject laserPrefab;
    public GameObject laserEmitter;

    public GameObject explosion;
    public AudioSource laserSource;

    private GameManager gameManager;

    public ParticleSystem engineGlow;

    // Start is called before the first frame update
    void Start()
    {
        
        mainCamera = Camera.main;
        myRigidbody = this.gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        //flyShip();
        //wrapPosition();

        if (Input.GetMouseButtonDown(0))
        {
            FireLaser();
        }

        if (Input.GetKey(KeyCode.W))
        {
            engineGlow.Play();
        }
        else
        {
            engineGlow.Stop();
        }




    }

    public void FireLaser()
    {
        // Adds recoil effect to ship.
        myRigidbody.AddRelativeForce(Vector3.back * 1, ForceMode.VelocityChange);


        // Checks to see if the front of ship is in view, it not no laser will fire.
        Vector3 emitterPos = mainCamera.WorldToViewportPoint(laserEmitter.transform.position);
        if (emitterPos.x > 1 || emitterPos.x < 0 || emitterPos.y > 1 || emitterPos.y < 0)
        {
            return;
        }

        // Laser sound
        laserSource.Play();

        // Instantiastes laser
        GameObject laser = GameObject.Instantiate(laserPrefab);

        // Sets laser position, rotation, parent and adds force.
        laser.transform.position = this.transform.position;
        laser.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        laser.transform.SetParent(GameObject.Find("LaserParent").transform);
        laser.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,0,90) * .5f, ForceMode.VelocityChange);
        
        }

    public void flyShip()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddRelativeForce(Vector3.forward * 3, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myRigidbody.AddRelativeForce(Vector3.back * 3, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            myRigidbody.AddRelativeForce(Vector3.left * 3, ForceMode.Acceleration);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            myRigidbody.AddRelativeForce(Vector3.right * 3, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.RotateAround(this.transform.position, this.transform.up, Time.deltaTime * -90f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.RotateAround(this.transform.position, this.transform.up, Time.deltaTime * 90f);
        }
    }


    public void wrapPosition()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(this.transform.position);

        if (screenPos.x <-1)
        {
            Vector3 newPos = new Vector3(2f, screenPos.y, screenPos.z);
            newPos = mainCamera.ViewportToWorldPoint(newPos);
            this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
        }
        if (screenPos.x > 2)
        {
            Vector3 newPos = new Vector3(-1, screenPos.y, screenPos.z);
            newPos = mainCamera.ViewportToWorldPoint(newPos);
            this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
        }
        if (screenPos.y < -1)
        {
            Vector3 newPos = new Vector3(screenPos.x, 2f, screenPos.z);
            newPos = mainCamera.ViewportToWorldPoint(newPos);
            this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
        }
        if (screenPos.y > 2)
        {
            Vector3 newPos = new Vector3(screenPos.x, -1, screenPos.z);
            newPos = mainCamera.ViewportToWorldPoint(newPos);
            this.transform.position = new Vector3(newPos.x, newPos.y, 0f);
        }

    }


    public void explodeMe()
    {
        GameObject boom = GameObject.Instantiate(explosion);
        boom.transform.position = this.transform.position;
        gameManager.alive = false;
        gameManager.lives -= 1;
        Destroy(boom, 1.99f);
        this.gameObject.SetActive(false);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            explodeMe();
        }
    }
}
