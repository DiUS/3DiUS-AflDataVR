using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

	public float speed;
	public float amplitude;
	Vector3 pos;
	float newX, newY;

	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		newX = pos.x + amplitude * Mathf.Cos(speed*Time.time);
		newY = pos.y + amplitude/2 * Mathf.Sin(speed/2*Time.time)/2;
		pos.x = newX;
		pos.y = newY;
		transform.position = pos;
	}
}
