using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResurrectionHUD : MonoBehaviour
{
	//Attributes
	[Header ("Tutorial Text")]
	public CanvasGroup tutorialText;
	[Header ("Distance Text")]
	public Text distance;
	[Header ("Fade")]
	public GameObject fadePanel;
	[Header ("Rotate")]
	public GameObject rotateButton;
    public GameObject phoneImage;
	public GameObject player;
	public GameObject spawner;
	public GameObject scenery;

	private static ResurrectionHUD instance = null;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
	}

	void Update() {
		distance.text = ((int)ResurrectionGameCtrl.Instance.distance).ToString ("D4");
	}

	//Public methods
	public static ResurrectionHUD Instance {
		get{ return instance; }
	}

	public void StartFade (float time, int sceneOrder)
	{
		fadePanel.SetActive (true);
		fadePanel.GetComponent<CanvasRenderer> ().SetAlpha (0f);
		StartCoroutine (FadeScene (time, sceneOrder));
	}

	public void RotateButton ()
	{
		rotateButton.SetActive (false);
        phoneImage.SetActive(false);
		player.SetActive (true);
		spawner.SetActive (true);
		scenery.SetActive (true);
		distance.gameObject.SetActive (true);
		StartCoroutine ("FadeTutorialText");
	}

	//Private methods
	private IEnumerator FadeScene (float time, int sceneOrder)
	{
		fadePanel.GetComponent<Image> ().CrossFadeAlpha (1f, time, false);
		yield return new WaitForSeconds (time);
		Destroy (ResurrectionGameCtrl.Instance.gameObject);
		SceneManager.LoadScene (sceneOrder);
	}

	private IEnumerator FadeTutorialText ()
	{
		tutorialText.alpha = 1f;
		yield return new WaitForSeconds (1.5f);
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds (0.1f);
			tutorialText.alpha -= 0.05f;
		}
	}
}