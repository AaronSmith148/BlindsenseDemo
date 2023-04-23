using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnDepthBuffer : MonoBehaviour {

    public Camera mainCamera;
    void Start()
    {
        mainCamera.depthTextureMode = DepthTextureMode.Depth;
    }
}
