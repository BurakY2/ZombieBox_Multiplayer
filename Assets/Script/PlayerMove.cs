using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class PlayerMove : Photon.Bolt.EntityBehaviour<IPlayerState>
{

    public CharacterController controller;
    public float MouseSensitivity = 50F;
    float xRotation = 0f;
    public float speed = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Camera cam;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform,gameObject.transform);
    }

    public override void SimulateOwner()
    {
        if(entity.IsOwner)
        {
            cam.gameObject.SetActive(true);
        }
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 0,0);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        /*if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }*/
    }
}
