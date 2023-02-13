using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    private Animator animator;
    
    private float velocity;

    private NavMeshAgent navMeshAgent; 
    
    public GameObject player;

    private static readonly int deathTrigger = Animator.StringToHash("Death");
    private static readonly int velocityFloat = Animator.StringToHash("velocity");

    private bool dead;
    
    private void Awake()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        if (dead) return;
        if(Vector3.Distance(player.transform.position, transform.position) < 10) navMeshAgent.SetDestination(player.transform.position);
        else navMeshAgent.ResetPath();
        animator.SetFloat(velocityFloat, navMeshAgent.velocity.magnitude);   
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 15) transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
    }

    public void Kill()
    {
        GetComponent<BoxCollider>().enabled = false;
        navMeshAgent.enabled = false;
        animator.SetTrigger(deathTrigger);
        dead = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.GameOver();
    }
}
