using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    private Animator _anim;

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _jumpHeight = 15f;
    [SerializeField]
    private float _gravity = 1f;

    private bool _jumping = false;

    private Vector3 _movement;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded == true)
        {
            float horiMoving = Input.GetAxisRaw("Horizontal");
            _movement = new Vector3(0, 0, horiMoving);
            _anim.SetFloat("Speed", Mathf.Abs(horiMoving));

            if (horiMoving != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _movement.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            _anim.SetBool("Jumping", false);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _movement.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }
        }
        else
        {
            _movement.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_movement *_speed * Time.deltaTime);
    }
}
