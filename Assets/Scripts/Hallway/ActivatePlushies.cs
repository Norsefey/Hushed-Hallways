using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Listens for the NavMesh to finish baking, then activates plushies
/// </summary>
public class ActivatePlushies : MonoBehaviour
{
	[SerializeField] GameObject Plushie;
	private void Start()
	{
		Baked.AddListener(Order66);
	}
	private void Order66()
	{
		Plushie.SetActive(true);
	}
}