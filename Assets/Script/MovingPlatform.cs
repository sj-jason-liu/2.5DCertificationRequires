using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //variables for both position
    [SerializeField]
    private Transform _targetA, _targetB;

    [SerializeField]
    private float _speed = 3f;

    private bool _moveToA;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moving between positions
        if(_moveToA)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, _targetA.position) <= 0)
            {
                _moveToA = !_moveToA;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetB.position) <= 0)
            {
                _moveToA = !_moveToA;
            }
        }
    }
}
