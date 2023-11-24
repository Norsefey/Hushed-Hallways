using System.Collections;
using UnityEngine;
/// <summary>
/// Idle state, monster does nothing or patrols after a set time
/// </summary>
public class StateIdle : State
{
    [SerializeField] float SensingRange = 8f; // Range at which the monster can see the player
    [SerializeField] float MinIdleTimer = 2f; // Minimum time before acting
    [SerializeField] float MaxIdleTimer = 5f; // Maximum time before acting
    float IdleTimer => Random.Range(MinIdleTimer, MaxIdleTimer); // Random time before acting
    private bool CanAct;
    public override void EnterState()
    {
        base.EnterState();
        // Set Monster's collider radius to sensing range
        GetComponent<SphereCollider>().radius = SensingRange;
        if (!CanAct) StartCoroutine(IdleTimerCoroutine());
    }
    public override void UpdateState()
    {
        // If can't act, do nothing
        if (!CanAct) return;
        // 50% chance to do nothing
        if (Random.Range(0, 100) < 50) Debug.Log("Monster is idle");
        // 50% chance to patrol
        else Monster.ChangeState(GetComponent<StatePatrol>());
    }
    public override void ExitState()
    {
        base.ExitState();
        // Reset CanAct bool
        CanAct = false;
        // Reset Monster's collider radius
        GetComponent<SphereCollider>().radius = 0.5f;
    }
    IEnumerator IdleTimerCoroutine()
    {
        Debug.Log("Monster is inactive for " + IdleTimer + " seconds");
        // Wait for timer to expire
        yield return new WaitForSeconds(IdleTimer);
        // Now can act
        CanAct = true;
        Debug.Log("Monster is active");
    }
}