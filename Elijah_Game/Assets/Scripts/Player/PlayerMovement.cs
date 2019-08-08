using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
   
    public float m_Speed = 12f; // How fast the Player moves forward and back
    public float s_speed; //Sprint Speed
    public float m_SpeedOrigional; //The origional speed
    public float m_TurnSpeed = 180f; // How fast the Player turns in degrees per second
    private float m_MovementInputValue; // The current value of the movement input
    private float m_TurnInputValue; // the current value of the turn input
    private CharacterController cc;

    //animation 
    public Animator animator;
    public float InputX;
    public float InputY;
    public float InputZ;
//    public bool Idle; 
    private UnityEngine.AI.NavMeshAgent m_NavAgent;
    private GameObject m_Player;



    void Start()
    {
        //get the animator 
        animator = this.gameObject.GetComponent<Animator>();
        m_SpeedOrigional = m_Speed;
    }


    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        // also reset the input values
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }



    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey("left shift") || Input.GetKey("right shift"))
        {
            m_Speed = s_speed;

            //animation
            InputY = Input.GetAxis("Vertical");
            //code to ajust animations
            animator.SetFloat("InputY", InputY * 2);

            //animation
            InputX = Input.GetAxis("Horizontal");
            //code to ajust animations
            animator.SetFloat("InputX", InputX * 2);
        }
        else
        {
            m_Speed = m_SpeedOrigional;
     //       animator.SetBool("Sprinting", false);


                //animation
        InputY = Input.GetAxis("Vertical");
        //code to ajust animations
        animator.SetFloat("InputY", InputY);

        //animation
        InputX = Input.GetAxis("Horizontal");
        //code to ajust animations
        animator.SetFloat("InputX", InputX);
        }


        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");





        //making transition between left -> idle -> right to just left -> Right by adding an "idle" bool
     //if(Idle == true)
     //   {
     //       animator.SetBool("Idle", true);
     //   }
     //else
     //   {
     //       animator.SetBool("Idle", false);
     //   }

     //   if (InputX > 0)
     //   {
     //       Idle = false;
     //   }
     //   else
     //   {
     //       Idle = true;
     //   }
     //   if (InputY > 0)
     //   {
     //       Idle = false;
     //   }
     //   else
     //   {
     //       Idle = true;
     //   }
     //   if (InputZ > 0)
     //   {
     //       Idle = false;
     //   }
     //   else
     //   {
     //       Idle = true;
     //   }
        Move();

    }
   
    private void Move()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // make it horizontal
        cameraForward.Normalize();

        // find the sideways direction
        Vector3 cameraSideways = new Vector3(cameraForward.z, 0, -cameraForward.x);

        // snap to the camera direction if moving forwards or strafing
        if (m_MovementInputValue > 0 || m_TurnInputValue != 0)
            transform.forward = cameraForward;


        //face the player backwards
        //if (Input.GetKey(KeyCode.Mouse1))
        //{

        //}
        //else
        //{
        //    if (m_MovementInputValue < 0)
        //        transform.forward = -cameraForward;
        //}

            // create a vector in the direction the tank is facing with a magnitude
            // based on the input, speed and time between frames
            Vector3 movement = (cameraForward * m_MovementInputValue + cameraSideways * m_TurnInputValue) * m_Speed;
            // Apply this movement to the rigidbody's position
            cc.SimpleMove(movement);
       
    }
  
}