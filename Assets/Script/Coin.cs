using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform _transform;

    [SerializeField]
    private float _rotateSpeed = 3f;
    
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _transform.Rotate(_rotateSpeed, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().AddCoin();
            Destroy(gameObject);
        }
    }
}
