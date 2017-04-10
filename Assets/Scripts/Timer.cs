using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public delegate void FinishedCountdown();
	public static event FinishedCountdown finishedCountdown;

	bool countDown;
	float timer;
	Text t;

	void OnEnable()
	{
		LevelManager.startTimer += StartTimer;
	}

	void OnDisable()
	{
		LevelManager.startTimer -= StartTimer;
	}

	// Use this for initialization
	void Start () {
		countDown = false;
		t = transform.GetComponent<Text>();
		t.enabled = false;
	}
	
	void LateUpdate () {
		if (countDown) {
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				countDown = false;
				t.enabled = false;
				finishedCountdown();
			}

			t.text = FormatTime(timer);

		}
	}

	string FormatTime(float timer) {
		int minutes = Mathf.FloorToInt(timer / 60F);
		int seconds = Mathf.FloorToInt(timer - minutes * 60);
		return string.Format("{0:0}:{1:00}", minutes, seconds);
	}

	void StartTimer(float timeToGo)
	{
		timer = timeToGo;
		t.enabled = true;
		countDown = true;
	}

}
