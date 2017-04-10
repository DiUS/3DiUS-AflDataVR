using LitJson;
using UnityEngine;
using System;

public class Players : MonoBehaviour
{
	public delegate void PlanetLoaded (GameObject planet,int playerId);
	public static event PlanetLoaded planetLoaded;
	public GameObject SolarSystem;
	public GameObject Planet;
	public GameObject Sun;
	public int gameMinimum;

	GameObject planets;

	JsonFetcher fetcher;

	void OnEnable ()
	{
		JsonFetcher.jsonPlayerData += ReadPlayerData;
	}

	void OnDisable ()
	{
		JsonFetcher.jsonPlayerData -= ReadPlayerData;
	}

	void Start ()
	{
		fetcher = GameObject.Find ("Fetcher").GetComponent<JsonFetcher> ();
	}

	public void PlayersForTeam (int teamId)
	{
		fetcher.GetJson (Urls.players_for_team (teamId.ToString ()), "players");
	}

	void ReadPlayerData (JsonReader json)
	{
		planets = new GameObject ("planets");
		float distanceFromSun = Sun.transform.localScale.x / 2; 

		Player[] jsonObject = JsonMapper.ToObject<Player[]> (json);
		foreach (var player in jsonObject) {

			int games = player.games;
			int diameter = Int16.Parse (player.height.Substring (0, 3)) / 10; 

			// pointless showing people who have not played yet!
			if (games > gameMinimum) {

				distanceFromSun += diameter;

				GameObject planet = (GameObject)Instantiate (Planet, Vector3.zero, Quaternion.identity);
				planet.transform.SetParent (planets.transform);
				planet.GetComponent<Planet> ().Setup (distanceFromSun, diameter, player);
				planet.name = player.first_name + " " + player.surname;
				planetLoaded (planet, player.id);
			}
		}

		planets.transform.SetParent (SolarSystem.transform);
	}
}
