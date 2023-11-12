using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Maintains a queue of nodes, moves the monster to the next node in the queue
/// Nodes expire after a certain time
/// </summary>
public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }
    [Header("Chasing Stats")]
    public bool isChasing = false; // is the monster chasing the player?
    [SerializeField] float ForgetTime = 10f; // time before the monster gives up chasing the player
    [SerializeField] float moveSpeed; // speed of the monster's movement
    [Header("Node Handling")]
    public List<Transform> GlobalNodes = new(); // list of nodes
    [SerializeField] float nodeExpireTime = 5f; // time before a node expires
    [SerializeField] float arrivedDistance = 0.1f; // distance between nodes
    readonly Queue<Transform> nodeQueue = new(); // queue of nodes
    #region Unity Methods
    private void Awake()
    {
        #region Singleton
        if (Instance == null) Instance = this;
        else Debug.LogError("Too many monsters in scene");
        #endregion
    }
    private void FixedUpdate()
    {
        DoMovement(); // move the monster to the next node
        if (Vector3.Distance(transform.position, nodeQueue.Peek().position) < arrivedDistance) nodeQueue.Dequeue(); // dequeue if close enough 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isChasing = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Invoke(nameof(GiveUp), ForgetTime); // give up after x seconds
    }
    #endregion
    #region Chase Methods
    private void DoMovement() // move the monster to the next node
    {
        Transform NextNode = GetNextNode(); // get the next node
        Vector3 targetVelocity = (NextNode.position - transform.position).normalized * (moveSpeed + nodeQueue.Count / 2); // calculate the target velocity
        Vector3 velocity = GetComponent<Rigidbody>().velocity; // get the monster's current velocity
        Vector3 velocityChange = (targetVelocity - velocity); // calculate the velocity change
        velocityChange.y = 0; // set the y velocity change to 0
        GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange); // add the velocity change to the monster
    }
    private void GiveUp() // stop chasing the player
    {
        isChasing = false; // stop chasing the player
        nodeQueue.Clear(); // clear the node queue
    }
    #endregion
    #region Node Handling
    private Transform GetNextNode()
    {
        if (nodeQueue.Count == 0) // if the queue is empty
        {
            if (!isChasing) AddNode(FindNearestNode()); // if not chasing player, enqueue the nearest node
            else AddNode(Player.Instance.transform); // enqueue the player
        }
        return nodeQueue.Peek(); // return the next node
    }
    private Transform FindNearestNode()
    {
        Transform nearestNode = null; // nearest node
        float nearestNodeDistance = Mathf.Infinity; // distance to nearest node
        foreach (Transform node in GlobalNodes) // for each node in the scene
        {
            float distance = Vector3.Distance(transform.position, node.position); // calculate the distance to the node
            if (distance < nearestNodeDistance) // if the distance is less than the nearest node distance
            {
                nearestNode = node; // set the nearest node to the current node
                nearestNodeDistance = distance; // set the nearest node distance to the current node distance
            }
        }
        return nearestNode; // return the nearest node
    }
    public void AddNode(Transform NewNode) // add a new node to the queue
    {
        nodeQueue.Enqueue(NewNode); // enqueue new node
        //Invoke(nameof(ExpireNode), nodeExpireTime); // expire the node after a certain time
    }
    protected void ExpireNode(Transform ExpiredNode) // expire a node, not implemented for now
    {
        // dequeue expired node
        if (nodeQueue.Count > 0 && nodeQueue.Peek() == ExpiredNode) nodeQueue.Dequeue();
    }
    #endregion
}