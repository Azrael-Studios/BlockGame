using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }
    public GameObject explosion;
    public GameObject deadMenu;
    public Controller playerController;
    public float range;
    private Transform player;
    private NavMeshAgent navAgent;
    private Vector3 lastPosition;
    private float speed;

    private float _attackDelay = 2.0f;
    private EnemyState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindObjectOfType<Controller>().transform;
        }

        navAgent = this.GetComponent<NavMeshAgent>();

        lastPosition = this.transform.position;
        navAgent.speed = Random.Range(10f, 20f);
        navAgent.SetDestination(player.position);
        _currentState = EnemyState.Chase;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState == EnemyState.Chase)
        {
            navAgent.SetDestination(player.position);
            speed = (this.transform.position - lastPosition).magnitude / Time.deltaTime;
            lastPosition = this.transform.position;
        }
        else if(_currentState == EnemyState.Attack)
        {
            Debug.Log("Shooting at the player");
        }
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(player.position, transform.position) <= range)
        {
            Explode();
        }
        else
        {
            _currentState = EnemyState.Chase;
        }
    }

    void Explode()
    {
        explosion.SetActive(true);
        var exp = explosion.GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
        navAgent.isStopped = true;
        playerController.TakeDamage(20);
        Debug.Log("Player health is " + playerController.health);
        this.enabled = false;
    }
}
