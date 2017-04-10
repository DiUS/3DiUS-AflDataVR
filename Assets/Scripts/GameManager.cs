using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static GameManager mInstance = null;
	
	// We separate out the game, bonus and end levels.
	// so these are set and got by those scene when needed.
	public int currentScore;
	int highScore;

	public static GameManager Instance {
		get {
			if (mInstance == null) {
				mInstance = GameObject.FindObjectOfType (typeof(GameManager)) as GameManager;
				if (mInstance == null)
					mInstance = new GameObject ("GameManager").AddComponent<GameManager> ();
			} 
			return mInstance;
		}
		
	}
	
	void Awake ()
	{
		// First we check if there are any other instances conflicting
		if (mInstance != null && mInstance != this) {
			// If that is the case, we destroy other instances
			Destroy (gameObject);
		}
		
		// Here we save our singleton instance
		mInstance = this as GameManager;
		Application.targetFrameRate = 60;
		DontDestroyOnLoad (transform.gameObject);
	}
	
	void OnApplicationQuit ()
	{
		Destroy (gameObject);
	}
	
	public void NewGame ()
	{
		highScore = 0;
		Application.LoadLevel ("Main");
	}

	public void EndGame ()
	{
		if (currentScore > highScore)
			highScore = currentScore;
	}

	public int HighestScore ()
	{
		return highScore;
	}

}