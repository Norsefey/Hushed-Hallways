using UnityEngine;
/// <summary>
/// Spawns the monster on a random waypoint near the player
/// </summary>
public class SpawnMonster : MonoBehaviour
{
    public static void Spawn()
    {
        Collider[] colliders = Physics.OverlapSphere(PlayerHealth.Instance.transform.position, 16);
        // Get a random waypoint
        Transform NearbyWaypoint = colliders[Random.Range(0, colliders.Length)].transform;
        // while the waypoint is too close to the player, get a new one
        while (Vector3.Distance(NearbyWaypoint.position, PlayerHealth.Instance.transform.position) < 8)
        {
            NearbyWaypoint = colliders[Random.Range(0, colliders.Length)].transform;
        }
        // Spawn the monster at the waypoint
        GameObject monster = Instantiate(Resources.Load<GameObject>("Prefabs/Monster"), NearbyWaypoint.position, Quaternion.identity);
        // Put the monster on the ground
        monster.transform.position = new(monster.transform.position.x, 0, monster.transform.position.z);
        // Put them in patrol mode
        monster.GetComponent<Monster>().ChangeState(monster.GetComponent<StatePatrol>());

        // Destroy this script
        Destroy(FindObjectOfType<SpawnMonster>());
    }
}