using UnityEngine;
/// <summary>
/// Spawns the monster on a random waypoint near the player
/// </summary>
public class SpawnMonster : MonoBehaviour
{
    public static void Spawn()
    {
        Collider[] colliders = Physics.OverlapSphere(Player.Instance.transform.position, 10);
        // Get a random waypoint
        Transform NearbyWaypoint = colliders[Random.Range(0, colliders.Length)].transform;

        // Spawn the monster at the waypoint
        Instantiate(Resources.Load<GameObject>("Prefabs/Monster"), NearbyWaypoint.position, Quaternion.identity);
    }
}