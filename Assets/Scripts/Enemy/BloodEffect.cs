using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private float _lifeTime = 1.5f;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Invoke("DisableWithTime", _lifeTime);
    }

    void DisableWithTime()
    {
        gameObject.SetActive(false);
        transform.parent = PoolManager.Instance.transform;
    }
}
