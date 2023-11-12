using UnityEngine;
/// <summary>
/// Assigned to node prefab, adds own transform to pathfinding queue
/// </summary>
public class Node : MonoBehaviour
{
    public bool isValid; // is the node valid?
    private void Start()
    {
        Pathfinding.Instance.GlobalNodes.Add(transform); // add the node to the global nodes list
        Debug.Log("Node added to global list");
        isValid = true; // set the node to valid
        if (!isValid) Debug.LogError(this.gameObject.name + "is invalid");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !Pathfinding.Instance.isChasing) return; // if the player is not in the trigger or the monster is not chasing the player, return
        Pathfinding.Instance.AddNode(transform);
        Debug.Log("Node added to queue");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster") && isValid) isValid = false; // if the node is valid, set it to invalid
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster")) isValid = true;
    }
}