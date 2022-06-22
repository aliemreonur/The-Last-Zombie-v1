using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > 200 || Mathf.Abs(transform.position.z) > 200)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish") || other.CompareTag("Environment"))
        {
            gameObject.SetActive(false);
            transform.position = PlayerController.Instance.transform.position;
        }
    }

    private void OnDisable()
    {
        transform.position = PoolManager.Instance.transform.position;
    }

}
