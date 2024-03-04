using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //type for rigidBody
    private Rigidbody rbPlayer;
    private Vector3 direction = Vector3.zero;

    //Player Variables
    public float speed = 10f;

    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        //player Movement
        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        direction = new Vector3(horMove,0 ,verMove);


    }

    private void FixedUpdate()
    {
        rbPlayer.AddForce(direction * speed,ForceMode.Force);
    }
}
