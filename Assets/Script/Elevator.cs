using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private GameObject _positionA, _positionB;
    private Transform _currentTarget;

    [SerializeField]
    private float _speed = 3f;

    private bool _isLiftUp;
    private bool _hasReached;

    // Update is called once per frame
    void FixedUpdate()
    {   
        switch(_isLiftUp)
        {
            case true:
                _currentTarget = _positionA.transform;
                break;
            case false:
                _currentTarget = _positionB.transform;
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, _currentTarget.position) <= 0 && !_hasReached)
        {
            _hasReached = true;
            StartCoroutine(DirectionSwitcher());
        }
    }

    IEnumerator DirectionSwitcher()
    {
        yield return new WaitForSeconds(5f);
        _isLiftUp = !_isLiftUp;
        yield return new WaitForSeconds(1f);
        _hasReached = false;
    }
}
