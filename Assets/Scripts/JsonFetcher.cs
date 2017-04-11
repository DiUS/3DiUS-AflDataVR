using System;
using System.Collections;
using UnityEngine;

public class JsonFetcher : MonoBehaviour
{
	string dataType;

	public delegate void JsonPlayerData (string json);
	public static event JsonPlayerData jsonPlayerData;
    public delegate void JsonPlayerStatsData (string json, GameObject g);
	public static event JsonPlayerStatsData jsonPlayerStatsData;

	public void GetJson (string url, string type, GameObject planet = null)
	{
		dataType = type;
        StartCoroutine(GetUrlData(url, planet));
    }

    IEnumerator GetUrlData(string url, GameObject planet)
    {
        WWW web = new WWW(url);
        while (!web.isDone)
        {
            yield return null;
        }

        if (web.error != null)
        {
            Debug.Log("Server Error: " + web.error);
        }
        else
        {
            Notify (web.text, planet);
        }
    }

    void Notify (string json, GameObject planet)
	{
		if (dataType == "players")
			jsonPlayerData (json);
		else
            jsonPlayerStatsData (json, planet);

	}
}