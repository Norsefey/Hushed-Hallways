using Unity.AI.Navigation;
using UnityEngine;
/// <summary>
/// Provides static method to bake the NavMesh, then self-destructs
/// </summary>
public class BakeNavMesh : MonoBehaviour
{
    public static void Bake()
    {
        Debug.Log("Baking NavMesh...");
        NavMeshSurface[] surfaces = FindObjectsOfType<NavMeshSurface>(); // Get all the NavMeshSurfaces in the scene
        foreach (NavMeshSurface surface in surfaces) surface.BuildNavMesh(); // Bake all the NavMeshSurfaces
        Debug.Log("NavMesh Baked");
        Destroy(FindObjectOfType<BakeNavMesh>()); // Destroy this script
    }
}