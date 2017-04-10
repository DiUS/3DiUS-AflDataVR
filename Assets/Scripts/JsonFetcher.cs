using LitJson;
using System;
using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using CielaSpike;

public class JsonFetcher : MonoBehaviour
{
	string dataType;

	public delegate void JsonPlayerData (JsonReader json);
	public static event JsonPlayerData jsonPlayerData;
	public delegate void JsonPlayerStatsData (JsonReader json,GameObject g);
	public static event JsonPlayerStatsData jsonPlayerStatsData;

	IEnumerator FetchJson (string url, GameObject g)
	{
		string data;
		using (WebClient client = new WebClient()) {
			data = client.DownloadString (url);
		}

		yield return Ninja.JumpToUnity;
		JsonReader json = new JsonReader (data);
		Notify (json, g);
	}

	public void GetJson (string url, string type, GameObject planet = null)
	{
		dataType = type;
		this.StartCoroutineAsync (FetchJson (url, planet));
	}

	void Notify (JsonReader json, GameObject g)
	{
		if (dataType == "players")
			jsonPlayerData (json);
		else
			jsonPlayerStatsData (json, g);

	}
}
