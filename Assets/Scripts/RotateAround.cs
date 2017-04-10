using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{
	float degrees;

	// Update is called once per frame
	void Start ()
	{
		degrees = Random.Range (1, 3);
		
	}
	void Update ()
	{
		transform.RotateAround (transform.parent.transform.position, Vector3.up, degrees * Time.deltaTime);
	}
}
