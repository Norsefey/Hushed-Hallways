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
    public State CurrentState = null; // Reference to the current state
    #endregion
    #region Operations
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // get reference
    }
    private void Update()
    {
        if (CurrentState != null) CurrentState.UpdateState();
        else ChangeState(GetComponent<StateIdle>());
    }
    private void OnTriggerEnter(Collider other)
    {
        // If player enters sensing range, chase
        if (!other.CompareTag("Player") || LineOfSight()) return;
        Debug.Log("Player detected, chasing...");
        ChangeState(GetComponent<StateChase>());
    }
    private bool LineOfSight() // Use a raycast to see if we have direct line of sight to the player
    {
        Vector3 direction = Player.Instance.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Check if the hit object is the player
            if (hit.collider.gameObject.CompareTag("Player")) return true;
        }
        return false;
    }
    public void ChangeState(State newState)
    {
        if (CurrentState != null) CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

    #endregion
}