using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody rb;
    private float walkSpeed = 1f;

    private const string IS_WALKING = "isWalking";
    private const string THROW = "throw";

    // Start is called before the first frame update
    void Start()
    {
        // initializing the variable
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
        // setting velocity to forward direction
        rb.velocity = Vector3.forward * walkSpeed;
        
        // rotating the player to forward direction
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);   

        // activating the walking animation
        animator.SetBool(IS_WALKING, true);    
    }

    public void MoveDown() 
    {
        // setting velocity to backward direction
        rb.velocity = -Vector3.forward * walkSpeed;

        // rotating the player to backward direction
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        // activating the walking animation
        animator.SetBool(IS_WALKING, true);
    }

    public void StopMoving()
    {
        // stopping the player movement
        rb.Sleep();

        // deactivating the walking animation
        animator.SetBool(IS_WALKING, false);
    }

    public void ThrowBalloon(Vector3 targetPos) 
    {
        // activating the throwing animation
        animator.SetTrigger(THROW);

        // rotating the player to the throw direction
        transform.LookAt(new Vector3(targetPos.x, 9f, targetPos.z), Vector3.up);
    }
}
