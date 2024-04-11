using System.Collections;
using System.Collections.Generic;
using Scirpts;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private int _waypointindex;
    private NavMeshAgent _navMeshAgent;

    private Enemy _enemy;
    private Transform dest;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        dest = GameObject.FindWithTag("Finish").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        
        _navMeshAgent.speed = _enemy.Speed;
        _navMeshAgent.destination = dest.position;
        var dist = dest.position - transform.position;
        if (dist.magnitude < 3)
        {
            EndPath();
        }
        
        _enemy.Speed = _enemy.startSpeed;
        
        
        /*Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * (_enemy.Speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            GetNextWaypoint();
        }

        _enemy.Speed = _enemy.startSpeed;*/
    }

    private void GetNextWaypoint()
    {
        if (_waypointindex >= Waypoints.WaypointsTransforms.Length - 1)
        {
            EndPath();
            return;
        }
        _waypointindex++;
        _target = Waypoints.WaypointsTransforms[_waypointindex];
    }

    private void EndPath()
    {
        WaveSpawner.enemiesAlives--;
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
