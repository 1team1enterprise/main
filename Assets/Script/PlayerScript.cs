using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    private Rigidbody2D rigid;

    public float speed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            Move();
        }
    }
    
    void Move()
    {
        float xPos = speed * Input.GetAxisRaw("Horizontal");
        float yPos = speed * Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(xPos, yPos);
    }
}