using UnityEngine;
/// <summary>
/// Abstract state class, inherited by all states
/// </summary>
public abstract class State : MonoBehaviour
{
    protected Monster Monster;
    [Tooltip("Speed to be added to the monster's base speed when entering this state")]
    [SerializeField] protected float StateSpeedModifier;
    private void Awake()
    {
        Monster = GetComponent<Monster>();
    }
    public virtual void EnterState()
    {
        Debug.Log("Entering state: " + GetType().Name);
        Monster.agent.speed = Monster.BaseSpeed += StateSpeedModifier;
    }
    public abstract void UpdateState();
    public virtual void ExitState()
    {
        Debug.Log("Exiting state: " + GetType().Name);
        Monster.CurrentState = null; // Reset current state
    }
}