using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// State for chasing the player
/// </summary>
public class StateChase : State
{
    [SerializeField] float MinChaseTimer = 5f; // Minimum time before giving up chase
    [SerializeField] float MaxChaseTimer = 10f; // Maximum time before giving up chase
    float ChaseTimer => Random.Range(MinChaseTimer, MaxChaseTimer); // Random time before giving up chase
    [Tooltip("Distance at which the monster will attack the player")]
    [SerializeField] float AttackRange = 2f; // Distance at which the monster will attack
    protected override void EnterState()
    {
        StartCoroutine(ChaseTimerCoroutine()); // Start timer
    }
    public override void UpdateState()
    {
        // Set player as destination
        Monster.agent.SetDestination(Player.Instance.transform.position);
        // Reduce speed as we get closer
        Monster.agent.speed = Mathf.Lerp(BaseStateSpeed, 0, Vector3.Distance(transform.position, Player.Instance.transform.position) / AttackRange);
        // If player is close enough, attack
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) < AttackRange)
        {
            Player.Instance.TakeDamage();
            ExitState();
        }
    }
    IEnumerator ChaseTimerCoroutine()
    {
        // Wait for timer to expire
        yield return new WaitForSeconds(ChaseTimer);
        // Give up chase
        ExitState();
    }
}