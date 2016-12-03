using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResurrectionGameCtrl : MonoBehaviour
{
	//Attributes
	[Header ("Distances")]
	public float distance;
	public float maxDistance;

	public float sunT = 0.0f;

	private static ResurrectionGameCtrl instance = null;
	private int score = 0;

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

	void Start ()
	{
		maxDistance = Random.Range (1500, 2001);
		distance = maxDistance;
	}

	// Update is called once per frame
	void Update ()
	{
		if (score > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", score);
		}
		distance -= Time.deltaTime * 50;
		if (distance < 0) {
			distance = 0;
		}
		sunT = 1.0f - distance / maxDistance;

		if (distance == 0) {
			SceneManager.LoadScene (1);
			Destroy (this.gameObject);
		}
	}

	//Public methods
	public static ResurrectionGameCtrl Instance {
		get{ return instance; }
	}
}
