using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    LayerMask m_LayerMask;

    Animator m_Animator;
    bool Mouse2;

    private void Awake()
    {
        m_LayerMask = LayerMask.GetMask("Border");
    }
    void Start()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    private void Update()
    {



        //look at
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
            transform.LookAt(hit.point);
            //remove the x and z components from rotation
            transform.eulerAngles = Vector3.up * transform.eulerAngles.y;

        }


        //animation
        if (Input.GetKey(KeyCode.Mouse1))
            //Send the message to the Animator to deactivate the trigger parameter named "Mouse2"
            m_Animator.SetBool("Mouse2", true);
        else
            //Send the message to the Animator to activate the trigger parameter named "Mouse2"
            m_Animator.SetBool("Mouse2", false);


    }
}
