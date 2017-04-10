using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterText : MonoBehaviour
{
	public string fullText = "The text you want shown on screen with typewriter effect.";
	public float letterSpeed = 10f; // speed of typewriter
	private float timeElapsed = 0;   
	private Text displayText;
	private Player player;

	void Start ()
	{
		displayText = transform.GetComponent<Text> ();
	}

	void OnEnable ()
	{
		RayCaster.detectedPlanet += ExtractData;
		LevelManager.setStartSectorMessage += SetStartSectorMessage;
	}

	void OnDisable ()
	{
		RayCaster.detectedPlanet -= ExtractData;
		LevelManager.setStartSectorMessage -= SetStartSectorMessage;
	}

	void Update ()
	{
		timeElapsed += Time.deltaTime;
		displayText.text = GetWords ((int)(timeElapsed * letterSpeed));
	}

	void SetStartSectorMessage (string sector)
	{
		fullText = "Sector: " + sector;
		timeElapsed = 0;
	}

	void ExtractData (GameObject g)
	{
		Player raycastPlayer = g.GetComponent<Planet> ().PlanetInformation ();
		if (raycastPlayer != player) {
			player = raycastPlayer;
			fullText = player.first_name + " " + player.surname;
			timeElapsed = 0;
		}
	}

	string GetWords (int letterCount)
	{
		int letters = letterCount;
		if (letters < fullText.Length)	
			return fullText.Substring (0, letters);

		return fullText;
	}

}
