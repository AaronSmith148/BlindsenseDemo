using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float speed = .5f;
    public Transform target;
    public Vector3 targetLocation;
    bool isMoving = false;
    public GameObject lightSource;
    private GameObject lightBall;
    private Animator animator;
    private AudioSource alertSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        alertSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
        if(transform.position.x == targetLocation.x && transform.position.z == targetLocation.z)
        {
            animator.SetBool("IsRunning", false);
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
            target = triggerCollider.transform;
            targetLocation = target.position;
            alertSound.Play();
            EnableLights();
            StartCoroutine(AnimationPlay());
            StartCoroutine(LookAtTarget(target));
        }
    }

    private void EnableLights()
    {
        lightBall = Instantiate(lightSource, new Vector3(0, 0, 0), Quaternion.identity);
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

    private IEnumerator LookAtTarget(Transform target)
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        //rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        float time = 0;
        float lookSpeed = 1f;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, time);
            time += Time.deltaTime * lookSpeed;
            yield return null;
        }
        
    }

    private IEnumerator AnimationPlay()
    {
        animator.SetBool("IsAlerted", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        animator.SetBool("IsAlerted", false);
        animator.SetBool("IsRunning", true);
        isMoving = true;
    }
}
