using UnityEngine;
/// <summary>
/// Abstract state class, inherited by all states
/// </summary>
public abstract class State : MonoBehaviour
{
    protected Monster Monster;
    [SerializeField] protected float BaseStateSpeed;
    private void Awake()
    {
        Monster = GetComponent<Monster>();
    }
    protected virtual void EnterState()
    {
        Debug.Log("Entering state: " + GetType().Name);
        Monster.agent.speed = BaseStateSpeed;
    }
    public abstract void UpdateState();
    protected virtual void ExitState()
    {
        Debug.Log("Exiting state: " + GetType().Name);
    }
    public void ChangeState(State newState)
    {
        Monster.CurrentState.ExitState();
        Monster.CurrentState = newState;
        Monster.CurrentState.EnterState();
    }
}