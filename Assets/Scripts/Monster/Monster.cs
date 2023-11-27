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
    [SerializeField] private float GracePeriod = 8; // How long the monster can't chase the player for
    public static bool CanChase = true; // Can the monster chase the player?
    public State CurrentState = null; // Reference to the current state
    public static float BaseSpeed = 4; // Base speed of the monster

    [SerializeField]
    GameObject hushVisual;

    #endregion
    #region Operations
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // get reference
    }
    private void Start()
    {
        Debug.Log("Monster can not chase for 8 seconds");
        BaseSpeed--; // Reduce base speed by 1 because we increment on spawn
        Invoke(nameof(AllowChasing), GracePeriod);
    }
    private void Update()
    {
        if (CurrentState != null) CurrentState.UpdateState();
        else ChangeState(GetComponent<StateIdle>());
    }
    private void OnTriggerEnter(Collider other)
    {
        // If player enters sensing range, chase
        if (!other.CompareTag("Player")) return; // If it's not the player, don't chase
        Debug.Log("Player is within sensing range");
        if (!LineOfSight()) return; // If we don't have line of sight, don't chase
        if (!CanChase) return; // If we can't chase, don't chase
        if (GetComponent<StateChase>().PlayerTransform == null) GetComponent<StateChase>().PlayerTransform = other.transform;
        ChangeState(GetComponent<StateChase>());
    }
    private bool LineOfSight() // Use a raycast to see if we have direct line of sight to the player
    {
        Debug.Log("LOS: CHECKING");
        Vector3 direction = Player.Instance.transform.position - transform.position;
        Ray ray = new(transform.position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Check if the hit object is the player
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("LOS: TRUE");
                return true;
            }
        }
        Debug.Log("LOS: FALSE");
        return false;
    }
    public void ChangeState(State newState)
    {
        if (CurrentState != null) CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    public void AllowChasing()
    {
        Debug.Log("Monster can now chase");
        CanChase = true;

        hushVisual.SetActive(true);
    }
    #endregion
}