using System.Collections;
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
    [HideInInspector] public Transform PlayerTransform = null;

    [SerializeField]
    GameObject hushVisual;

    public override void EnterState()
    {
        base.EnterState();
        StartCoroutine(ChaseTimerCoroutine()); // Start timer
    }
    public override void UpdateState()
    {
        // Set player as destination
        Monster.agent.SetDestination(PlayerTransform.position);
        // Reduce speed as we get closer
        //Monster.agent.speed = Mathf.Lerp(BaseStateSpeed, 1, Vector3.Distance(transform.position, PlayerHealth.Instance.transform.position) / AttackRange);
        // If player is close enough, attack
        if (Vector3.Distance(transform.position, PlayerTransform.position) < AttackRange)
        {
            PlayerHealth.Instance.TakeDamage();

            //after the monster attacks, he disappears//hide his render until he can attack again
            hushVisual.SetActive(false);
            Monster.CanChase = false;
            Monster.Invoke(nameof(Monster.AllowChasing), 8);

            ExitState();
        }
    }
    IEnumerator ChaseTimerCoroutine()
    {
        //Debug.Log("Monster while give up in " + ChaseTimer + " seconds");
        // Wait for timer to expire
        yield return new WaitForSeconds(ChaseTimer);
        //Debug.Log("Giving up chase");
        // Give up chase
        ExitState();
    }
}