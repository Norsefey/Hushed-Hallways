using UnityEngine;
/// <summary>
/// Patrolling behaviour, moves to a new node, waits, then exits state
/// </summary>
public class StatePatrol : State
{
    [SerializeField] private float SensingRange = 4f; // Range at which the monster can see the player
    public override void EnterState()
    {
        base.EnterState();
        // Set sphere collider to sensing range
        GetComponent<SphereCollider>().radius = SensingRange;
        // Set destination to new position
        Monster.agent.SetDestination(FindPosition());
    }
    public override void UpdateState()
    {
        // If we're close enough to the destination, exit state
        if (Vector3.Distance(transform.position, Monster.agent.destination) < Monster.agent.stoppingDistance) ExitState();
    }
    public override void ExitState()
    {
        base.ExitState();
        // Reset sphere collider
        GetComponent<SphereCollider>().radius = 0.5f;
    }
    private Vector3 FindPosition()
    {
        // Get all waypoints
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        // If there are no waypoints, return current position
        if (waypoints.Length == 0) return transform.position;
        // Get a random waypoint
        GameObject waypoint = waypoints[Random.Range(0, waypoints.Length)];
        // If the waypoint is the same as the previous waypoint, get a new one
        if (waypoint.transform == Monster.PrevWaypoint) return FindPosition();
        // Set previous waypoint to this one
        Monster.PrevWaypoint = waypoint.transform;
        // Return the waypoint's position
        return waypoint.transform.position;
    }
}