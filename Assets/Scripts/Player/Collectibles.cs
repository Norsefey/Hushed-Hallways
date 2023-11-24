using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public int plushiesCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plushy"))
        {
            plushiesCollected++;
            Destroy(other.gameObject);
            if (plushiesCollected == 1) SpawnMonster.Spawn(); // Spawn monster when first plushie is collected
            Monster.BaseSpeed++; // Increase monster speed when plushie is collected
        }
    }
}
