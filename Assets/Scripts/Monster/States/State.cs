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
    public virtual void EnterState()
    {
        Debug.Log("Entering state: " + GetType().Name);
        Monster.agent.speed = BaseStateSpeed;
    }
    public abstract void UpdateState();
    public virtual void ExitState()
    {
        Debug.Log("Exiting state: " + GetType().Name);
        Monster.CurrentState = null; // Reset current state
    }
}