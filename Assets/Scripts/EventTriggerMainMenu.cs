using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventTriggerMainMenu : MonoBehaviour
{
	//Attributes
	[Header ("Panels")]
	public GameObject highScorePanel;
	public GameObject creditsPanel;
	public GameObject junctionLogo;
	[Header ("Text")]
	public Text highScore;
	[Header ("Buttons")]
	public GameObject playButton;
	public GameObject scoreButton;
	public GameObject creditsButton;
	public GameObject backMenuButton;
	[Header ("Fade Panel")]
	public GameObject fadePanel;
	[Header ("Phoenix Mesh")]
	public GameObject birdEnter;
	public GameObject birdExit;
    public GameObject birdFly;
    public GameObject birdFlyHighScore;
	[Header ("Sound FX")]
	public AudioSource soundFx;

	private bool loading = false;
	private float angle = 0f;

	void Update() {
		if (loading) {
			angle += Time.deltaTime * 250;
			Quaternion qt = junctionLogo.transform.rotation;
			qt.eulerAngles = new Vector3 (0f, 0f, angle);
			junctionLogo.transform.rotation = qt;
		}
	}

	public void PlayGame ()
	{
		loading = true;
		backMenuButton.SetActive (false);
        birdFly.SetActive(false);
        birdFlyHighScore.SetActive(false);
        HideButtons (true);
		birdEnter.SetActive (false);
		birdExit.SetActive (true);
		soundFx.Play ();
		StartCoroutine ("WaitPlay");
	}
		
	public void ShowHighScore ()
	{
		HideButtons (true);
        birdEnter.SetActive(false);
        birdFly.SetActive(false);
        birdFlyHighScore.SetActive(true);
		soundFx.Play ();
		highScorePanel.SetActive (true);
		highScore.text = PlayerPrefs.GetInt ("HighScore").ToString ();
	}

	public void ShowCredits ()
	{
		HideButtons (true);
        birdEnter.SetActive(false);
        birdFlyHighScore.SetActive(false);
        birdFly.SetActive(true);
        soundFx.Play ();
		creditsPanel.SetActive (true);
	}

	public void ExitGame ()
	{
		soundFx.Play ();
		StartCoroutine ("WaitExit");
	}

	public void BackToMainMenu ()
	{
		highScorePanel.SetActive (false);
		creditsPanel.SetActive (false);
		HideButtons (false);
	}

	private IEnumerator WaitExit ()
	{
		yield return new WaitForSeconds (0.5f);
		Application.Quit ();
	}

	private IEnumerator WaitPlay ()
	{
		yield return new WaitForSeconds (1f);
		fadePanel.SetActive (true);
		fadePanel.GetComponent<CanvasRenderer> ().SetAlpha (0f);
		fadePanel.GetComponent<Image> ().CrossFadeAlpha (1f, 0.5f, false);
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene (1);
	}

	private void HideButtons (bool hide)
	{
		playButton.SetActive (!hide);
		scoreButton.SetActive (!hide);
		creditsButton.SetActive (!hide);
	}
}
