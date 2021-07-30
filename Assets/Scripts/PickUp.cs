using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameManager gameManager; 
    private int xForce;
    private int yForce;
    private Rigidbody myRigidbody;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCamera = gameManager.mainCamera;
        addForce();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void addForce()
    {
        //Gives random values for force to apply to asteroid.
        xForce = Random.Range(-200, 200);
        yForce = Random.Range(-200, 200);

        //Adds force to asteroid based on random values which causes asteroids to move around level.
        myRigidbody.AddForce(xForce, yForce, 0, ForceMode.Force);

        //Applies random torque/rotation to asteroid so it spins around.
        myRigidbody.AddRelativeTorque(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    public void checkDestroy(Vector3 pos)
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(pos);
        if(screenPos.x > 1.03 || screenPos.x < -.03 || screenPos.y > 1.03 || screenPos.y < -.03)
        {
            Destroy(this.gameObject);
        }
    }

    //Detects collision with Trigger (ships are only things with triggers)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit trigger");
        pickMeUp();
    }

    public void pickMeUp()
    {
            gameManager.lives += 1;
            Destroy(this.gameObject);
    }
    
}
