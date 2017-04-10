using LitJson;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
	public GameObject pillar;
	public Material material;
	public GameObject explosion;

	JsonFetcher fetcher;

	void OnEnable ()
	{
		Players.planetLoaded += GetStats;
		JsonFetcher.jsonPlayerStatsData += ReadPlayerData;
	}

	void OnDisable ()
	{
		Players.planetLoaded -= GetStats;
		JsonFetcher.jsonPlayerStatsData -= ReadPlayerData;
	}

	void Start ()
	{
		fetcher = GameObject.Find ("Fetcher").GetComponent<JsonFetcher> ();
	}

	void GetStats (GameObject planet, int playerId)
	{
		fetcher.GetJson (Urls.player_stats (playerId.ToString ()), "stats", planet);
	}

	void ReadPlayerData (JsonReader json, GameObject playerPlanet)
	{
		JsonData data = JsonMapper.ToObject (json);
		int count = data ["stats"].Count;
		float rowNum = (int)Mathf.Floor (Mathf.Sqrt (count));
		float maxX = rowNum / 2; 
		float x = -1 * maxX;
		float z = x;

		GameObject cityBlock = new GameObject ();
		cityBlock.AddComponent<MeshFilter> ();
		cityBlock.AddComponent<MeshRenderer> ();

		for (int i = 0; i < count; i++) {
			float height = (int)data ["stats"] [i] ["disposals"];
			Vector3 pos = new Vector3 (x++, 0, z);
			GameObject building = (GameObject)Instantiate (pillar, pos, Quaternion.identity);
			building.transform.localScale = new Vector3 (0.9f, height, 0.9f);
			building.transform.SetParent (cityBlock.transform);
			if (x == maxX) {
				x = -1 * maxX;
				z++;
			}
		}

		CombineChildMeshes (cityBlock);

		cityBlock.transform.SetParent (playerPlanet.transform);

		BuildCityBlock (cityBlock);

		cityBlock.AddComponent<CityBlock> ();
		cityBlock.AddComponent<CityBlockCollisions> ();
		cityBlock.GetComponent<CityBlockCollisions> ().SetScore (count);
	}
	
	void CombineChildMeshes (GameObject g)
	{
		MeshFilter[] meshFilters = g.transform.GetComponentsInChildren<MeshFilter> ();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		int i = 0;
		while (i < meshFilters.Length) {
			combine [i].mesh = meshFilters [i].sharedMesh;
			combine [i].transform = meshFilters [i].transform.localToWorldMatrix;
			meshFilters [i].gameObject.active = false;
			i++;
		}
		Mesh mesh = new Mesh ();
		g.transform.GetComponent<MeshFilter> ().mesh = mesh;
		mesh.CombineMeshes (combine);
		mesh.RecalculateNormals ();
		g.transform.gameObject.SetActive (true);
	}

	void BuildCityBlock (GameObject cb)
	{
		cb.transform.localPosition = new Vector3 (0, 0.5f, 0);
		cb.transform.localScale /= 2;
		cb.transform.rotation = Quaternion.identity;
		cb.GetComponent<Renderer> ().material = material;
		cb.AddComponent<BoxCollider> ();
		cb.GetComponent<BoxCollider> ().isTrigger = true;
		cb.layer = LayerMask.NameToLayer ("city");	

		GameObject particles = (GameObject)Instantiate (explosion, cb.transform.position, Quaternion.identity);
		particles.transform.SetParent (cb.transform);
	}

}