using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Add plushies to static list
/// </summary>
public class ActivatePlushies : MonoBehaviour
{
	[SerializeField] GameObject Plushie;
	private void Start()
	{
		HallwayGenerator.Plushies.Add(Plushie);
	}
}