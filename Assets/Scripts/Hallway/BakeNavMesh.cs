using Unity.AI.Navigation;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
/// <summary>
/// Provides static method to bake the NavMesh, then self-destructs
/// </summary>
public class BakeNavMesh : MonoBehaviour
{
	private UnityEvent<string> BakedEvent = new();

    public static void Bake(List<NavMeshSurface> newHalls)
    {
        Debug.Log("Baking NavMesh...");
        foreach (NavMeshSurface surface in newHalls) surface.BuildNavMesh(); // Bake all the NavMeshSurfaces
        Debug.Log("NavMesh baked, invoking event...");

	BakedEvent.Invoke();

        Destroy(FindObjectOfType<BakeNavMesh>()); // Destroy this script
    }
}