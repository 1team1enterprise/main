using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player2Script : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(-0.1f, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.1f, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0.0f, 0.1f, 0.0f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0.0f, -0.1f, 0.0f);
            }
        }



    }


}