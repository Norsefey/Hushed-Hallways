using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Assigned to the monster, gathers context and delegates to the current state
/// </summary>
public class Monster : MonoBehaviour
{
    #region Attributes
    public NavMeshAgent agent; // Reference to NavMeshAgent component
    public static Transform PrevWaypoint; // Reference to the previous waypoint
    public State CurrentState; // Reference to the current state
    #endregion
    #region Operations
    private void Awake()
    {
        // Get references
        agent = GetComponent<NavMeshAgent>();
        // Validate states
        if (GetComponent<StateIdle>() == null) gameObject.AddComponent<StateIdle>();
        if (GetComponent<StatePatrol>() == null) gameObject.AddComponent<StatePatrol>();
        if (GetComponent<StateChase>() == null) gameObject.AddComponent<StateChase>();
    }
    private void Update()
    {
        if (CurrentState != null) CurrentState.UpdateState();
        else CurrentState = GetComponent<StateIdle>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // If player enters sensing range, chase
        if (other.CompareTag("Player")) CurrentState.ChangeState(GetComponent<StateChase>());
    }
    #endregion
}