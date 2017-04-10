using UnityEngine;
using System.Collections;

public class CityBlockCollisions : MonoBehaviour
{

	GameObject smallExplosion;
	GameObject largeExplosion;
	int score = 0;
	float health = 100;

	void Start ()
	{
		smallExplosion = Resources.Load ("Prefabs/smallExplosion") as GameObject;
		largeExplosion = Resources.Load ("Prefabs/largeExplosion") as GameObject;
	}

	// set when instantiated, is the players total games played
	public void SetScore (int score)
	{
		this.score = score;
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "laser") {
			health -= 5;
			if (health <= 0) {
				GameObject bigExplosion = (GameObject)Instantiate (largeExplosion, transform.position, Quaternion.identity);
				GameManager.Instance.currentScore += score;
				Destroy (transform.gameObject);
			} else {
				GameObject littleExplosion = (GameObject)Instantiate (smallExplosion, transform.position, Quaternion.identity);
			}
		}
	}
}