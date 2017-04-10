using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour
{
	public delegate void DetectedPlanet (GameObject planet);
	public static event DetectedPlanet detectedPlanet;
	public float rayDistance;

	void Update ()
	{
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.forward * rayDistance, Color.red, 0.1f);
		if (Physics.Raycast (transform.position, transform.forward, out hit, rayDistance)) {
			detectedPlanet (hit.transform.gameObject);
		}
	}
}