using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoResolution : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(1440, 2560);
    public float referenceCameraSize = 5f;

    void Start()
    {
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        var mainCamera = GetComponent<Camera>();

        // Get the screen resolution
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calculate the aspect ratios
        float referenceAspectRatio = referenceResolution.x / referenceResolution.y;
        float currentAspectRatio = screenWidth / screenHeight;

        // Adjust the camera size based on the aspect ratio
        if (currentAspectRatio >= referenceAspectRatio)
        {
            mainCamera.orthographicSize = referenceCameraSize;
        }
        else
        {
            float differenceInSize = referenceAspectRatio / currentAspectRatio;
            mainCamera.orthographicSize = referenceCameraSize * differenceInSize;
        }
    }
}
