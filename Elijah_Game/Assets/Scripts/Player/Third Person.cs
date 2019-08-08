using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    public float m_Speed = 12f; // How fast the tank moves forward and back
    public float m_SpeedOrigional;
    public float m_TurnSpeed = 180f; // How fast the tank turns in degrees per second
    private float m_MovementInputValue; // The current value of the movement input
    private float m_TurnInputValue; // the current value of the turn input
    private CharacterController cc;

    //animation 
    public Animator animator;
    public float InputX;
    public float InputY;
    public float InputZ;
    private UnityEngine.AI.NavMeshAgent m_NavAgent;
    private GameObject m_Player;


    //dodge
    public float Spedup = 0f;

    public float m_NormalSpeed;
    public float m_DodgeSpeed;

    public Renderer[] body;
    public Material dodgeMaterial;
    Material[] baseMaterials;
    public GameObject DodgeParticle;
    public GameObject Particles;

    public GameObject Fade;






    void Start()
    {
        //get the animator 
        animator = this.gameObject.GetComponent<Animator>();
        m_SpeedOrigional = m_Speed;

        // store the original materials
        baseMaterials = new Material[body.Length];
        for (int i = 0; i < body.Length; i++)
            baseMaterials[i] = body[i].material;

        DodgeParticle.SetActive(false);
        Fade.SetActive(false);

        Spedup = -50f;
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

    private void Dodge()
    {

        Particles.SetActive(false);
        DodgeParticle.SetActive(true);
        Fade.SetActive(true);
        /*
                if ((Input.GetKey(KeyCode.LeftShift)) && (timeleft < 0)) //and there is no cooldown
                {
                    m_Speed = m_Speed * 1.05f;
                    Debug.Log("sadas");
                }


                if (timeleft > 0)
                {
                    timeleft -= Time.deltaTime;
                } 
                */
        /*
                m_Speed = m_Speed * 2f;
                TimeStamp = Time.time + Cooldown;
                PlayerMesh.SetActive(false);
                WaitForSeconds(3.0f);
                PlayerMesh.SetActive(true);
                */
        m_Speed = m_Speed + Spedup;

    }

    // Update is called once per frame
    private void Update()
    {
        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");


        //animation
        InputY = Input.GetAxis("Vertical");
        //code to ajust animations
        animator.SetFloat("InputY", InputY);

        //animation
        InputX = Input.GetAxis("Horizontal");
        //code to ajust animations
        animator.SetFloat("InputX", InputX);


        if (Input.GetKey(KeyCode.LeftShift) && (Spedup <= -50))
        {
            Spedup = 5f;
            Dodge();
        }

        Spedup -= Time.deltaTime * 12f;
        if (Spedup > 0)
        {
            m_Speed = m_DodgeSpeed;
            for (int i = 0; i < body.Length; i++)
                body[i].material = dodgeMaterial;
        }
        else
        {
            m_Speed = m_NormalSpeed;
            // restore the original materials
            for (int i = 0; i < body.Length; i++)
                body[i].material = baseMaterials[i];
            //reset particles
            Particles.SetActive(true);
            DodgeParticle.SetActive(false);
            Fade.SetActive(false);
        }




    }
    private void FixedUpdate()
    {
        Move();
        //Turn();
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
        if (Input.GetKey(KeyCode.Mouse1))
        {

        }
        else
        {
            if (m_MovementInputValue < 0)
                transform.forward = -cameraForward;
        }
        // create a vector in the direction the tank is facing with a magnitude
        // based on the input, speed and time between frames
        Vector3 movement = (cameraForward * m_MovementInputValue + cameraSideways * m_TurnInputValue) * m_Speed;
        // Apply this movement to the rigidbody's position
        cc.SimpleMove(movement);

    }
    private void Turn()
    {
        // determine the number of degrees to be turned based on the input,
        // speed and time between frames
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        // make this into a rotation in the y axis
        transform.eulerAngles += turn * Vector3.up;
    }

}