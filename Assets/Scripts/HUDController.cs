using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
	//Attributes
	[Header ("Texts")]
	public Text scoreText;
	[Header ("Tutorial")]
	public CanvasGroup tutorialImage;
	[Header ("Fade")]
	public GameObject fadePanel;

	private static HUDController instance = null;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
	}

	// Use this for initialization
	void Start ()
	{
		scoreText.text = "Score: " + GameController.Instance.score.ToString ("D4") + " ";
		StartCoroutine ("FadeTutorialImage");
	}

	//Public methods
	public static HUDController Instance {
		get{ return instance; }
	}

	public void UpdateScore ()
	{
		scoreText.text = "Score: " + GameController.Instance.score.ToString ("D4");
	}

	public void StartFade (float time, int sceneOrder)
	{
		fadePanel.SetActive (true);
		fadePanel.GetComponent<CanvasRenderer> ().SetAlpha (0f);
		StartCoroutine (FadeScene (time, sceneOrder));
	}

	//Private methods
	private IEnumerator FadeScene (float time, int sceneOrder)
	{
		fadePanel.GetComponent<Image> ().CrossFadeAlpha (1f, time, false);
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene (sceneOrder);
	}

	private IEnumerator FadeTutorialImage ()
	{
		tutorialImage.alpha = 1f;
		yield return new WaitForSeconds (1.5f);
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds (0.1f);
			tutorialImage.alpha -= 0.05f;
		}
	}
}
