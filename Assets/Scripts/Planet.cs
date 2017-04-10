using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
	public Material[] surfaces;
	Player planetInformation;

	void Start ()
	{
		GetComponent<Renderer> ().material = surfaces [Random.Range (0, surfaces.Length - 1)];
	}

	public void Setup (float distanceFromSun, float diameter, Player player)
	{
		planetInformation = player;
		transform.localScale *= diameter;
		transform.position = RandomCircle (distanceFromSun);
	}

	public Player PlanetInformation()
	{
		return planetInformation;
	}

	Vector3 RandomCircle (float radius)
	{
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = radius * Mathf.Sin (ang * Mathf.Deg2Rad);
		pos.y = 0;
		pos.z = radius * Mathf.Cos (ang * Mathf.Deg2Rad);
		return pos;
	}

}
