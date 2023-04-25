using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnThrowableOnButtonPress : MonoBehaviour
{
    public float throwSpeed = 1f;
    GameObject ballInstance;

    public void SpawnThrowable(GameObject throwablePrefab)
    {
       
        
        ballInstance = Instantiate(throwablePrefab, gameObject.transform.position, gameObject.transform.rotation);
        Physics.IgnoreCollision(ballInstance.GetComponent<Collider>(), GetComponent<Collider>());
        ballInstance.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throwSpeed);
        StartCoroutine(DestroyBall());
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(8);
        Destroy(ballInstance);
        ballInstance = null;
    }
}
