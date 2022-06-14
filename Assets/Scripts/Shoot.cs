using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Singleton<Shoot>
{
    /// <summary>
    /// Better to move the shoot behaviour to gun game object (OR THE HAND - SO THAT IT WHILL CHANGE THE ROTATION AND DIRECTION WITHOUT THE MATTER OF THE WEAPON
    /// </summary>
    // Start is called before the first frame update

    Vector3 lookVector;

    private void LookAt()
    {
        if (PlayerController.Instance.rotate.ReadValue<Vector2>() != Vector2.zero)
        {
            lookVector = new Vector3(PlayerController.Instance.rotate.ReadValue<Vector2>().x, PlayerController.Instance.rotate.ReadValue<Vector2>().y, transform.rotation.z);
            transform.rotation = Quaternion.LookRotation(lookVector, Vector3.up);
        }

    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
    }


}
