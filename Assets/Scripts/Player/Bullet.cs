using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        //disable functionality will be added.
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
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

    //Disable on hit zombie or end points.

}
