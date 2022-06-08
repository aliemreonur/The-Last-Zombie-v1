using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] float _maxZSpeed = 3f;
    [SerializeField] GameObject _bloodPrefab;
    private float _zSpeed;

    private bool _isChasingPlayer;

    private void Start()
    {
        _zSpeed = Random.Range(0, _maxZSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(_bloodPrefab, transform.position, transform.rotation);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COllision!");
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet!");
            Instantiate(_bloodPrefab, other.GetContact(0).point, transform.rotation);
            other.gameObject.SetActive(false); //disable the bullet again
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        //random dir
    }

    public void ChasePlayer()
    {
        //this will be only called from the child obj. 
        Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, _zSpeed*Time.deltaTime);
    }

}
