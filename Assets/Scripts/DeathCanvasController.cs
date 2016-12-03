using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathCanvasController : MonoBehaviour
{
	//Attributes
	public GameObject fadePanel;
	public Text playerScore;
	public Text highScore;
	public AudioSource soundFx;

	void Start ()
	{
		fadePanel.SetActive (true);
		fadePanel.GetComponent<CanvasRenderer> ().SetAlpha (0f);
		playerScore.text = GameController.Instance.score.ToString ("D4");
		highScore.text = PlayerPrefs.GetInt ("HighScore").ToString ("D4");
	}

	//Public methods
	public void OnRestartPointerDown ()
	{
		soundFx.Play ();
		StartCoroutine ("FadeScene", 1);
	}

	public void OnMenuPointerDown ()
	{
		soundFx.Play ();
		StartCoroutine ("FadeScene", 0);
	}

	//Private methods
	private IEnumerator FadeScene (int sceneOrder)
	{
		yield return new WaitForSeconds (0.2f);
		fadePanel.GetComponent<Image> ().CrossFadeAlpha (1f, 1.3f, false);
		yield return new WaitForSeconds (1.3f);
		if (GameController.Instance != null) {
			Destroy (GameController.Instance.gameObject);
		}
		SceneManager.LoadScene (sceneOrder);
	}
}
