using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Singleton<Shoot>
{
    /// <summary>
    /// This behaviour is postponed for now - did not get the perfect play experience.
    /// Needs clamping
    /// </summary>

    Vector3 lookVector;

    private void LookAt()
    {
        if (PlayerController.Instance.rotate.ReadValue<Vector2>() != Vector2.zero)
        {
            lookVector = new Vector3(PlayerController.Instance.rotate.ReadValue<Vector2>().x, PlayerController.Instance.rotate.ReadValue<Vector2>().y, transform.rotation.z);
            transform.rotation = Quaternion.LookRotation(lookVector, Vector3.up);
        }

    }

    void Update()
    {
        LookAt();
    }


}
