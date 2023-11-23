using UnityEngine;
/// <summary>
/// Assigned to player, handle being attacked by monster
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public int Health = 3;
    private void Awake()
    {
        #region Singleton
        if (Instance == null) Instance = this;
        else Debug.LogError("Too many players in scene");
        #endregion
    }
    public void TakeDamage()
    {
        Debug.Log("Player taking damage");
        Health--;
        if (Health <= 0) Destroy(gameObject);
    }
    private void OnDestroy()
    {
        return; // implement later
    }
}