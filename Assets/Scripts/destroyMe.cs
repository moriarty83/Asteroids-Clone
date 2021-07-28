﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour
{
    public float destroyTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyTimer > 0)
        {
            destroyTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
