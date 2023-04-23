using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowreelSpin : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TwoRotations(1));
    }

    IEnumerator TwoRotations(float waitTime)
    {
        StartCoroutine(Rotate(4));

        yield return new WaitForSeconds(waitTime + 4);
        
        StartCoroutine(Rotate(4));
    }
    IEnumerator Rotate(float duration)
    {

        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        yield return new WaitForSeconds(duration);
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }

    }
}
