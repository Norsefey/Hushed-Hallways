using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectibles : MonoBehaviour
{
    public static int plushiesCollected = 0;
    public static int plushiesNeedToWIn = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plushy"))
        {
            plushiesCollected++;
            Destroy(other.gameObject);
            if (plushiesCollected == 1) SpawnMonster.Spawn(); // Spawn monster when first plushie is collected
            else if(plushiesCollected > plushiesNeedToWIn) SceneManager.LoadScene(2);//load scene 2 which is win scene
            
            Monster.BaseSpeed++; // Increase monster speed when plushie is collected
        }
    }
}
