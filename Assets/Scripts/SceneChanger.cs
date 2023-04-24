using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level1End")
        {
            SceneManager.LoadScene("End Screen");
        }
    }
}
