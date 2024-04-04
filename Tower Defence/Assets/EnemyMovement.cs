using System.Collections;
using System.Collections.Generic;
using Scirpts;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private int _waypointindex;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = Waypoints.WaypointsTransforms[0];
    }
    
    private void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * (_enemy.Speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            GetNextWaypoint();
        }

        _enemy.Speed = _enemy.startSpeed;
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
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
