using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Robot : MonoBehaviour
{
    private Animator animator;
    
    private float velocity;

    private NavMeshAgent navMeshAgent; 
    
    public GameObject player;
    
    public InputAction velocityInput;
    public InputAction killInput;
    
    private static readonly int deathTrigger = Animator.StringToHash("Death");
    private static readonly int velocityFloat = Animator.StringToHash("velocity");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;

        killInput.performed += KillInput;
        
        velocityInput.Enable();
        killInput.Enable();
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 5) navMeshAgent.SetDestination(player.transform.position);
        else navMeshAgent.ResetPath();
        animator.SetFloat(velocityFloat, navMeshAgent.velocity.magnitude);   
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5) transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
    }

    private void KillInput(InputAction.CallbackContext context)
    {
        animator.SetTrigger(deathTrigger);
    }
}
