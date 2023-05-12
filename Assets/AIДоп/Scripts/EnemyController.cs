using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _playerTranform;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _sightRange;
    [SerializeField] private float _walkRange;
    [SerializeField] private float _approachRange;

    private NavMeshAgent _navMeshAgent;

    private Vector3 _walkPoint;
    [SerializeField] private bool _walkPointSet;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (IsPlayerInSight())
        {
            MoveToTarget(_playerTranform.position);
        }
        else
        {
            Patrol();
        }
        Vector3 direction = _playerTranform.position - transform.position;
        float angleInDegrees = 30f; // Góc 30 độ
        Quaternion rotationQuaternion = Quaternion.Euler(0f, Random.Range(-angleInDegrees, angleInDegrees), 0f);


        Vector3 rotatedDirection = rotationQuaternion * direction.normalized * Random.Range(0f, _approachRange);

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f),0f, Random.Range(-1f, 1f));
        Vector3 randomPosition = rotatedDirection + randomDirection;

        Debug.DrawLine(transform.position, rotatedDirection, Color.red);
        Debug.DrawLine(randomPosition, randomPosition + Vector3.up, Color.red, 3f);

    }

    private void Patrol()
    {
        Vector3 directionToPoint = _walkPoint - transform.position;
        if (directionToPoint.magnitude < 1f)
        {
            _walkPointSet = false;
        }

        if (_walkPointSet)
        {
            MoveToTarget(_walkPoint);
        }
        else SearchPoint();
    }

    private void SearchPoint()
    {

        Vector3 direction = _playerTranform.position - transform.position;


        bool player = Physics.Raycast(transform.position, direction, _approachRange, _playerMask);
        print(player);
        if(player)
        {
            //float angleInDegrees = 30f; // Góc 30 độ
            //Quaternion rotationQuaternion = Quaternion.Euler(0f, Random.Range(-angleInDegrees, angleInDegrees), 0f);


            //Vector3 rotatedDirection = rotationQuaternion * direction.normalized;
            //Debug.DrawLine(transform.position, rotatedDirection, Color.red);


            //_walkPoint = transform.position + rotatedDirection * _approachRange;
            //Debug.DrawLine(_walkPoint, _walkPoint + Vector3.up, Color.red, 6f);
        }
        else
        {
            float randomOffsetX = Random.Range(-_walkRange, _walkRange);
            float randomOffsetZ = Random.Range(-_walkRange, _walkRange);

            _walkPoint = new Vector3(transform.position.x + randomOffsetX, transform.position.y, transform.position.z + randomOffsetZ);
        }
        NavMeshPath path = new NavMeshPath();
        _navMeshAgent.CalculatePath(_walkPoint, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            _walkPointSet = true;
            Debug.DrawLine(_walkPoint, _walkPoint + Vector3.up, Color.green, 3f);
        }
        else
            Debug.DrawLine(_walkPoint, _walkPoint + Vector3.up, Color.red, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(_walkRange * 2, transform.position.y, _walkRange * 2));
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _approachRange);
    }

    private void MoveToTarget(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    private bool IsPlayerInSight()
    {
        Vector3 direction = _playerTranform.position - transform.position;

        Debug.DrawLine(transform.position, transform.position + direction.normalized * _sightRange, Color.magenta);
        Debug.DrawLine(transform.position + Vector3.up, transform.position + direction.normalized * direction.magnitude, Color.blue);

        bool player = Physics.Raycast(transform.position, direction, _sightRange, _playerMask);
        bool obstacle = Physics.Raycast(transform.position, direction, direction.magnitude,
            ~_playerMask);
        Debug.DrawRay(transform.position, direction,Color.green);
        if (!(!player | obstacle))
        {
            print("player: " + player);
            print("obstacle: " + obstacle);
        }
        return !(!player | obstacle);
    }
}
