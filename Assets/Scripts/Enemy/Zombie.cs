using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] float _maxZSpeed = 3f;
    [SerializeField] GameObject _bloodPrefab;
    private float _zSpeed;
    private WaitForSeconds hitResetTime = new WaitForSeconds(0.5f);

    [SerializeField] private int _zHealth = 3;
    private bool _isHit;
    private bool _isAttacking;
    private bool _isAlerted;

    public int ZHealth
    {
        get
        {
            return _zHealth;
        }
    }

    public bool IsHit
    {
        get
        {
            return _isHit;
        }
    }

    public bool IsAlive
    {
        get
        {
            return _zHealth > 0;
        }
    }

    public bool IsAttacking
    {
        get { return _isAttacking; }
        set { _isAttacking = value; }
    }

    public bool IsAlerted
    {
        get
        {
            return _isAlerted;
        }
        set
        {
            _isAlerted = value;
        }
    }
    

    private bool _isChasingPlayer;

    /// <summary>
    /// Enemy States:
    /// 1- Idle
    /// 2- Patroling
    /// 3- Chasing Player
    /// 4- Attacking
    /// 5- Being Attacked
    /// 6- Dead
    /// </summary>

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
        if (other.gameObject.CompareTag("Bullet"))
        {
            _isHit = true;
            other.gameObject.SetActive(false);
            Debug.Log("Bullet!");
            Instantiate(_bloodPrefab, other.GetContact(0).point, transform.rotation, transform);
            Damage();
            
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

    public void Damage()
    {
        Debug.Log("Damage");
        _zHealth--;
        if(_zHealth<0)
        {
            Destroy(this.gameObject, 1f);
        }

    }


    IEnumerator HitResetRoutine()
    {
        if(_isHit)
        {
            yield return hitResetTime;
            _isHit = false;
        }
 
    }

}
