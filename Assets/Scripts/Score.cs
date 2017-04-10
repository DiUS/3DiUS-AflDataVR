using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{

	public bool isHighestScore;
	Text t;

	void OnEnable ()
	{
		WarpEffect.startSector += ShowScore;
		LevelManager.startWarp += HideScore;
	}

	void OnDisable ()
	{
		WarpEffect.startSector -= ShowScore;
		LevelManager.startWarp -= HideScore;
	}

	void Start ()
	{
		t = transform.GetComponent<Text> ();

		if (isHighestScore)
			t.text = GameManager.Instance.HighestScore ().ToString ();
		else 
			t.enabled = false;
	}
	
	void Update ()
	{
		if (!isHighestScore)
			t.text = GameManager.Instance.currentScore.ToString (); 
	}

	void ShowScore ()
	{
		t.enabled = true;
	}

	void HideScore ()
	{
		t.enabled = false;
	}
}
