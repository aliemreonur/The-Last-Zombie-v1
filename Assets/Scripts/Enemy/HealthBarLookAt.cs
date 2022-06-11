using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLookAt : MonoBehaviour
{
    /// <summary>
    /// This script is for making sure that the health bar canvases does not rotate with the parent obj.
    /// Flip the image rotation to the opposite if not going to use this script.
    /// </summary>

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount %4 == 0)
        {
            transform.rotation = Quaternion.identity;
        }
 
        /*
        Vector3 posAccordingToCam = _mainCam.transform.position - transform.position;
        posAccordingToCam.x = posAccordingToCam.z = 0;
        transform.LookAt(posAccordingToCam);
        transform.Rotate(0, 180, 0);
        */
    }
}
