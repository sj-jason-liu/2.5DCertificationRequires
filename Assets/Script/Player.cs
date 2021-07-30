using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    private Animator _anim;

    private int _coin;

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _jumpHeight = 15f;
    [SerializeField]
    private float _gravity = 1f;
    private float vertiMoving;

    private bool _jumping = false;
    private bool _onLedge = false;
    private bool _onLadder = false;

    private Ledge _activeLedge;

    private Ladder _activeLadder;

    private Vector3 _movement;

    [SerializeField]
    private GameObject _model;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(_onLedge)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("ClimbUp");
            }
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            float horiMoving = Input.GetAxisRaw("Horizontal");
            _movement = new Vector3(0, 0, horiMoving);
            _anim.SetFloat("Speed", Mathf.Abs(horiMoving));

            if (horiMoving != 0)
            {
                Vector3 facing = _model.transform.localEulerAngles;
                facing.y = _movement.z > 0 ? 0 : 180;
                _model.transform.localEulerAngles = facing;
            }

            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            _anim.SetBool("Jumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _movement.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }
        }
        else if(_onLadder)
        {
            vertiMoving = Input.GetAxisRaw("Vertical");
            _movement = new Vector3(0, vertiMoving, 0);
            _anim.SetFloat("VertiSpeed", vertiMoving);
        }
        else
        {
            _movement.y -= _gravity * Time.deltaTime;
        }
        _controller.Move(_movement * _speed * Time.deltaTime);
    }

    public void GrabLedge(Vector3 snapPos, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetFloat("Speed", 0f);
        _anim.SetBool("Jumping", false);
        _onLedge = true;
        transform.position = snapPos;
        _activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
        _onLedge = false;
    }

    public void ReachedLadder()
    {
        _onLadder = !_onLadder;
        _anim.SetBool("ReachedLadder", _onLadder);
    }

    public void ClimbUpLadder(Ladder currentLadder)
    {
        _anim.SetTrigger("ClimbUpLadder");
        _controller.enabled = false;
        _activeLadder = currentLadder;
    }

    public void LadderComplete()
    {
        transform.position = _activeLadder.GetStandPos();
        _anim.SetBool("ReachedLadder", false);
        _controller.enabled = true;
        _onLadder = false;
    }

    public void AddCoin()
    {
        _coin++;
        UIManager.Instance.CoinUpdate(_coin);
    }
}
