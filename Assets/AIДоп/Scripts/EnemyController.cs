using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _playerTranform;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _sightRange;
    [SerializeField] private float _walkRange;

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
        float randomOffsetX = Random.Range(-_walkRange, _walkRange);
        float randomOffsetZ = Random.Range(-_walkRange,_walkRange);

        _walkPoint = new Vector3(transform.position.x + randomOffsetX, transform.position.y, transform.position.z + randomOffsetZ);

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
        bool obstacle = Physics.Raycast(transform.position, direction, direction.magnitude, _playerMask);

        return !(!player | obstacle);
    }

    private void SetDestinaltionToMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _navMeshAgent.SetDestination(hit.point);
        }
    }
}
