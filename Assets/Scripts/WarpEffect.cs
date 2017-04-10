using UnityEngine;
using System.Collections;

public class WarpEffect : MonoBehaviour
{

	public delegate void StartSector ();
	public static event StartSector startSector;
	float timer = 6.0f;
	bool countdown = true;

	void OnEnable ()
	{
		LevelManager.startWarp += StartWarp;
	}

	void OnDisable ()
	{
		LevelManager.startWarp -= StartWarp;
	}

	// Update is called once per frame
	void Update ()
	{
		if (countdown) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				transform.GetComponent<ParticleSystem> ().Stop ();
				startSector ();
				countdown = false;
			}
		}
	}

	void StartWarp ()
	{
		timer = 6.0f;
		transform.GetComponent<ParticleSystem> ().Clear ();
		transform.GetComponent<ParticleSystem> ().Play ();
		countdown = true;
	}
}
