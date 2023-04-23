using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float speed = .5f;
    public Vector3 targetLocation;
    bool isMoving = false;
    public GameObject lightSource;
    private GameObject lightBall;

    void Update()
    {
        
        if (isMoving)
        {
            Move();
        }
        if(transform.position.x == targetLocation.x && transform.position.z == targetLocation.z)
        {
            isMoving = false;
            DisableLights();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        lightBall.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        if(triggerCollider.tag == "SoundSource")
        {
            targetLocation = triggerCollider.transform.position;
            targetLocation.y += 1;
            isMoving = true;
            EnableLights();
        }
        if (triggerCollider.tag == "Player")
        {
            Debug.Log("Trigger Player");
            SceneManager.LoadScene("End Screen");
        }
    }

    private void EnableLights()
    {
        lightBall = Instantiate(lightSource, new Vector3(0, 0, 0), Quaternion.identity);
        lightBall.transform.localScale += new Vector3(1, 1, 1);
        lightBall.tag = "Untagged";
    }
    private void DisableLights()
    {
        Destroy(lightBall);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerCollider")
        {
            Debug.Log("Collision Player");
            SceneManager.LoadScene("End Screen");
        }
    }
}
