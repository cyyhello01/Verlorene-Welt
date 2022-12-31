using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    // The Agent component
    private NavMeshAgent agent;

    public float actionWaitTime = 4.0f;
    public float rotateTime = 3.0f;
    public float walkSpeed = 3.5f;
    public float runSpeed = 7.0f;

    public float enemyViewRadius = 15.0f;
    public float enemyViewAngle = 90.0f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1.0f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    private int _currentWaypointIndex;
    private Vector3 _targetWaypoint;

    private Vector3 _playerLastPosition = Vector3.zero;
    private Vector3 _playerLastSeenPosition;
    
    private float _waitTime;                          //  Variable of the wait time that makes the delay
    private float _timeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay
    private bool _playerInRange;                           //  If the player is in range of vision, state of chasing
    private bool _playerNear;                              //  If the player is near, state of hearing
    private bool _isPatrol;                                //  If the enemy is patrol, state of patroling
    private bool _caughtPlayer;                            //  if the enemy has caught the player
    
    void Start()
    {
        _playerLastSeenPosition = Vector3.zero;
        _isPatrol = true;
        _caughtPlayer = false;
        _playerInRange = false;
        _waitTime = actionWaitTime;
        _timeToRotate = rotateTime;

        _currentWaypointIndex = 0;
        agent = GetComponent<NavMeshAgent>();

        agent.isStopped = false;
        agent.speed = walkSpeed;
        agent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnvironmentView();
        Vector3 forward = agent.transform.TransformDirection(Vector3.forward) * enemyViewRadius;
        Debug.DrawRay(agent.transform.position, forward, Color.green);

        if (!_isPatrol)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }

    
    void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }

    void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    void CaughtPlayer()
    {
        _caughtPlayer = true;
    }

    private void Patrolling()
    {
        if (_playerNear)
        {
            if (-_timeToRotate <= 0)
            {
                Move(walkSpeed);
                LookingForPlayer(_playerLastPosition);
            }
            else
            {
                {
                    Stop();
                    _timeToRotate -= Time.deltaTime;
                }
            }
        }
        else
        {
            _playerNear = false;
            _playerLastPosition = Vector3.zero;
            agent.SetDestination(waypoints[_currentWaypointIndex].position);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (_waitTime <= 0)
                {
                    NextPoint();
                    Move(walkSpeed);
                    _waitTime = actionWaitTime;
                }
                else
                {
                    Stop();
                    _waitTime -= Time.deltaTime;
                }
            }
        }
    }
    
    void LookingForPlayer(Vector3 player)
    {
        agent.SetDestination(player);

        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (_waitTime <= 0)
            {
                _playerNear = false;
                Move(walkSpeed);
                agent.SetDestination(waypoints[_currentWaypointIndex].position);
                _waitTime = actionWaitTime;
                _timeToRotate = rotateTime;
            }
            else
            {
                Stop();
                _waitTime -= Time.deltaTime;
            }
        }
    }
    
    public void NextPoint()
    {
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    // The enemy has detected the player and is now chasing the player
    private void Chasing()
    {
        // _playerNear is false because enemy already see player
        _playerNear = false;
        _playerLastPosition = Vector3.zero;

        if (!_caughtPlayer)
        {
            Move(runSpeed);
            agent.SetDestination(_playerLastSeenPosition);
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (_waitTime <= 0 && !_caughtPlayer && Vector3.Distance(transform.position,
                    GameObject.FindGameObjectWithTag("Player").transform.position) >= 6.0f)
            {
                _isPatrol = true;
                _playerNear = false;
                Move(walkSpeed);
                _timeToRotate = rotateTime;
                _waitTime = actionWaitTime;
                agent.SetDestination(waypoints[_currentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position,
                        GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                }

                _waitTime -= Time.deltaTime;
            }
        }
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, enemyViewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToPlayer) < enemyViewAngle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    _playerInRange = true;
                    _isPatrol = false;
                }
                else
                {
                    _playerInRange = false;
                }
            }

            if (Vector3.Distance(transform.position, player.position) > enemyViewRadius)
            {
                _playerInRange = false;
            }

            if (_playerInRange)
            {
                _playerLastSeenPosition = player.transform.position;
            }
        }
    }
}
