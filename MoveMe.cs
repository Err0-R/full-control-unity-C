 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class MoveMe : MonoBehaviour {
     public float speed = 6.0F;
     public float jumpSpeed = 8.0F; 
     public float gravity = 20.0F;
     private Vector3 moveDirection = Vector3.zero;
     private float turner;
     private float looker;
     public float sensitivity;

     public float mouseSensitivity = 100.0f;
     public float clampAngle = 80.0f;
 
     private float rotY = 0.0f; // rotation around the up/y axis
     private float rotX = 0.0f;


     // Use this for initialization
     void Start () 
     {
         Cursor.visible = false;
         Cursor.lockState = CursorLockMode.Locked;

         Vector3 rot = transform.localRotation.eulerAngles;
         rotY = rot.y;
         rotX = rot.x;         
     }
     
     // Update is called once per frame
     void Update ()
     {

         CharacterController controller = GetComponent<CharacterController>();
         // is the controller on the ground?
         if (controller.isGrounded) {
             //Feed moveDirection with input.
             moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
             moveDirection = transform.TransformDirection(moveDirection);
             //Multiply it by speed.
             moveDirection *= speed;
             //Jumping
             if (Input.GetButton("Jump"))
                 moveDirection.y = jumpSpeed;
 
         }
         turner = Input.GetAxis ("Mouse X")* sensitivity;
         looker = -Input.GetAxis ("Mouse Y")* sensitivity;
         if(turner != 0){
             //Code for action on mouse moving right
             transform.eulerAngles += new Vector3 (0,turner,0);
         }
         if(looker != 0){
             //Code for action on mouse moving right
             transform.eulerAngles += new Vector3 (looker,0,0);
         }
         //Applying gravity to the controller
         moveDirection.y -= gravity * Time.deltaTime;
         //Making the character move
         controller.Move(moveDirection * Time.deltaTime);


                  float mouseX = Input.GetAxis("Mouse X");
         float mouseY = -Input.GetAxis("Mouse Y");
 
         rotY += mouseX * mouseSensitivity * Time.deltaTime;
         rotX += mouseY * mouseSensitivity * Time.deltaTime;
 
         rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
 
         Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
         transform.rotation = localRotation;
     }
 }