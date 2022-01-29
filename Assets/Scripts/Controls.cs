using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    CollisionHandler collisionHandler;

    // Start is called before the first frame update
    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionHandler.switchCollisionsOnOff();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            collisionHandler.LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
