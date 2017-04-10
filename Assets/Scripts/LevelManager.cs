using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	public delegate void StartWarp ();
	public delegate void SetStartSectorMessage (string sector);
	public delegate void StartTimer(float time);
	public static event StartWarp startWarp;
	public static event SetStartSectorMessage setStartSectorMessage;
	public static event StartTimer startTimer;
	public float minutesPerSector;
	public GameObject sun;
	public GameObject scoreText;
	public GameObject timerText;

	string[] teams = new string[] {
	    "Adelaide", "Brisbane", "Carlton", "Collingwood",
	    "Essendon", "Fremantle", "GWS", "Geelong",
	    "Gold Coast", "Hawthorn", "Melbourne", "North Melbourne",
	    "Port Adelaide", "Richmond", "St Kilda", "Sydney",
	    "West Coast", "Western Bulldogs"
	};

	int currentTeam = 1;

	private Players players;
	
	void OnEnable ()
	{
		WarpEffect.startSector += StartSector;
		Timer.finishedCountdown += EndSector;
	}

	void OnDisable ()
	{
		WarpEffect.startSector -= StartSector;
		Timer.finishedCountdown -= EndSector;
	}

	void Start ()
	{
		players = GameObject.Find ("Players").GetComponent<Players> ();
		minutesPerSector *= 60;
		EndSector();
	}

	void StartSector ()
	{
		Activate(true);
		players.PlayersForTeam (currentTeam);
		setStartSectorMessage (teams [currentTeam - 1]);
		startTimer(minutesPerSector);
		currentTeam++;
	}

	void EndSector ()
	{
		GameObject g = GameObject.Find("planets");
		if (g) Destroy (g);
		Activate(false);
		startWarp();
	}

	void Activate(bool b)
	{
		timerText.SetActive(b);
		scoreText.SetActive (b);
		sun.SetActive (b);
	}
}
