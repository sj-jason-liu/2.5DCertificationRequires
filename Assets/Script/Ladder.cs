using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private GameObject _standPosR, _standPosL;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.ReachedLadder();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ClimbUpLadder(this);
            }
        }
    }

    public Vector3 GetStandPosR()
    {
        return _standPosR.transform.position;
    }

    public Vector3 GetStandPosL()
    {
        return _standPosL.transform.position;
    }
}
