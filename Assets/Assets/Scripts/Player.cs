using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody rb;
    private float walkSpeed = 1f;
    private bool facingForward = true;

    private const string IS_WALKING = "isWalking";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z >= 0.699f) // clamping max movement coordinates
        {
            transform.position = new Vector3 (0f, 9f, 0.699f);
        }
        else if(transform.position.z <= -2.941f) // clamping min movement coordinates
        {
            transform.position = new Vector3 (0f, 9f, -2.941f);
        }
    }

    public void MoveUp() 
    {
        rb.velocity = Vector3.forward * walkSpeed;
        
        if(!facingForward) {
            facingForward = true;
            RotatePlayer();
        }
        animator.SetBool(IS_WALKING, true);

        
    }

    public void MoveDown() 
    {
        rb.velocity = -Vector3.forward * walkSpeed;

        if(facingForward) {
            facingForward = false;
            RotatePlayer();
        }

        animator.SetBool(IS_WALKING, true);
    }

    public void StopMoving()
    {
        rb.Sleep();
        animator.SetBool(IS_WALKING, false);
    }

    public void RotatePlayer() 
    {
        transform.Rotate(new Vector3(0f, 180f, 0f));
    }
}
