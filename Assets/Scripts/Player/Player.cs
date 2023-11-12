using UnityEngine;
/// <summary>
/// Assigned to player, handle being attacked by monster
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private void Awake()
    {
        #region Singleton
        if (Instance == null) Instance = this;
        else Debug.LogError("Too many players in scene");
        #endregion
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster")) // if the player is attacked by the monster
        {
            Debug.Log("Player attacked by monster");
            Destroy(gameObject); // destroy the player
        }
    }
    private void OnDestroy()
    {
        return; // implement later
    }
}