using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnThrowableOnButtonPress : MonoBehaviour
{
    public InputActionReference throwReference = null;
    public GameObject throwablePrefab;
    public float throwSpeed = 1f;
    
    // Update is called once per frame
    void Update()
    {
        
        throwReference.action.performed += SpawnThrowable;
    }

    private void SpawnThrowable(InputAction.CallbackContext context)
    {
        GameObject ballInstance = Instantiate(throwablePrefab);
        Vector3 direction = Vector3.forward;
        Vector3 velocity = throwSpeed * direction;
        ballInstance.GetComponent<Rigidbody>().velocity = velocity;
    }
}
