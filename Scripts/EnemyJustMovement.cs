using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyJustMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private EnemyMovement enemy;

    private void Start()
    {
        enemy = GetComponent<EnemyMovement>();
        target = Waypoints.points[0];
    }
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
