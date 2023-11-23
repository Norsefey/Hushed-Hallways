using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
    [SerializeField] float SensingRange = 8f; // Range at which the monster can see the player
    [SerializeField] float MinIdleTimer = 5f; // Minimum time before acting
    [SerializeField] float MaxIdleTimer = 10f; // Maximum time before acting
    float IdleTimer => Random.Range(MinIdleTimer, MaxIdleTimer); // Random time before acting
    private bool CanAct;
    protected override void EnterState()
    {
        // Set Monster's collider radius to sensing range
        GetComponent<SphereCollider>().radius = SensingRange;
        CanAct = false;
        StartCoroutine(IdleTimerCoroutine());
    }
    public override void UpdateState()
    {
        if (!CanAct) return; // If can't act, do nothing

        // 70% chance to do nothing
        if (Random.Range(0, 100) < 70) return;
        // 30% chance to patrol
        else ChangeState(GetComponent<StatePatrol>());
    }
    protected override void ExitState()
    {
        // Reset Monster's collider radius
        GetComponent<SphereCollider>().radius = 0.5f;
    }
    IEnumerator IdleTimerCoroutine()
    {
        // Wait for timer to expire
        yield return new WaitForSeconds(IdleTimer);
        // Now can act
        CanAct = true;
    }
}