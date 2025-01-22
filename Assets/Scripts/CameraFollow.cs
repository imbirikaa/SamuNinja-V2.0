using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 10f;
    public Transform target;





    void Update()
    {

        if (target != null)
        {

            Vector3 newPos = new Vector3(target.position.x, -1.5f, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }


    }

}
