using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class laser : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject);

        if (collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.GetComponent<asteroid>().explodeMe();
            Destroy(this.gameObject);

        }
        if (collision.gameObject.tag == "1up")
        {
            Destroy(this.gameObject);
        }

    }
}
