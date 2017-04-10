using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation *= Quaternion.AngleAxis (0.01f, Vector3.up);
	}
}
