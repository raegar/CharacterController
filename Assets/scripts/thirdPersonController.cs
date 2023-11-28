using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class thirdPersonController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public float rotationspeed = 100.0f;

    private float gravity = -9.81f;//earths gravity
    private CharacterController characterController;
    private NavMeshAgent player;
    private Vector3 Velocity;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(0, 0, vertical).normalized;

        //rotate character
        if(horizontal != 0)
        {
            transform.Rotate(Vector3.up, horizontal * rotationspeed * Time.deltaTime);
            player.ResetPath();
        }
        //move character
        if(direction.magnitude>= 0.1f)
        {
            if(vertical >= 0)
            {
                player.Move(transform.forward * vertical * movementSpeed * Time.deltaTime);
                player.ResetPath();

            }
            else
            {
                player.Move(-transform.forward * -vertical * movementSpeed * Time.deltaTime);
                player.ResetPath();
            }
        }

        //gravity apply
        if(characterController.isGrounded && Velocity.y < 0)
        {
            Velocity.y = 0;
        }

        Velocity.y += gravity * Time.deltaTime;
        player.Move(Velocity * Time.deltaTime);

        //upadtes animation peramitors

        if(animator != null)
        {
            animator.SetFloat("speed", Mathf.Abs(vertical));
            animator.SetFloat("direction", horizontal);
        }
    }
}
