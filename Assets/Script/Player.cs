using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //moving float
    //jumping
    //direction
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _jumpHeight = 15f;
    [SerializeField]
    private float _gravity = 1f;

    private Vector3 _movement;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded == true)
        {
            float horiMoving = Input.GetAxis("Horizontal");
            _movement = new Vector3(0, 0, horiMoving);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _movement.y += _jumpHeight;
            }
        }
        else
        {
            _movement.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_movement *_speed * Time.deltaTime);
    }
}
