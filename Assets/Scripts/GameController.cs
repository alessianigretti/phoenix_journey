using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	//Attributes
	[Header ("Score")]
	public int score = 0;

	private static GameController instance = null;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		if (score > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
	}

	//Public methods
	public static GameController Instance {
		get{ return instance; }
	}

	public void AddScore (int score)
	{
		this.score += score;
		HUDController.Instance.UpdateScore ();
	}
}
