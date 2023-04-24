using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLight : MonoBehaviour
{
    //settings for light generation
    public AudioSource collisonAudioSource;
    public GameObject lightSource;
    public float lightRange = 1f;
    public float timer = 5f;
    public float lightHoldTime = 1f;
    public GameObject[] enemies;

    //internal class variables
    private bool lightCreated = false;
    GameObject lightBall = null;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "ConstantSound")
        {
            lightBall = Instantiate(lightSource, gameObject.transform.position, Quaternion.identity);
            lightBall.transform.localScale += new Vector3(lightRange, lightRange, lightRange);
            lightCreated = true;
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Physics.IgnoreCollision(enemy.GetComponent<Collider>(), GetComponent<Collider>());
        }
        
    }

    private void Update()
    {
        if (gameObject.tag == "ConstantSound")
        {
            lightBall.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (!lightCreated)
        {
            StartCoroutine(LightSequence());
            CollisionSound();
        }
        
    }

    IEnumerator LightSequence()
    {
        
        yield return StartCoroutine(CreateLight());
        yield return new WaitForSeconds(lightHoldTime);
        yield return StartCoroutine(ScaleLight());
        yield return StartCoroutine(DestroyLight());

    }

    private void CollisionSound()
    {
        //play collision audio on collision
        collisonAudioSource.Play();
    }

    IEnumerator CreateLight()
    {
        //create light source and set its initial size
        lightBall = Instantiate(lightSource, gameObject.transform.position, Quaternion.identity);
        lightBall.transform.localScale = new Vector3(lightRange, lightRange, lightRange);
        lightBall.GetComponentInChildren<Light>().range = lightRange / 2;
        lightCreated = true;
        yield return null;
        
    }

    IEnumerator ScaleLight()
    {
        //shrink light source over the course of the timer duration
        float currentTimer = timer;
        float currentScale = lightRange;
        while (currentTimer >= 0)
        {
            currentScale = lightRange * (currentTimer / timer);
            lightBall.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            lightBall.GetComponentInChildren<Light>().range = currentScale / 2;
            currentTimer -= Time.deltaTime;
        }
        yield return null;
    }

    IEnumerator DestroyLight()
    {
        Destroy(lightBall);
        lightBall = null;
        lightCreated = false;
        yield return null;
    }

    public void LightEnterHand()
    {
        //make held object visible in player hand
        gameObject.layer = LayerMask.NameToLayer("Illuminated");
    }

    public void LightLeaveHand()
    {
        //make held object affected by lighting when dropped/thrown
        gameObject.layer = 0;
    }
}
