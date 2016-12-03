using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
	//Public methods
	public void OnTriggerEnter (Collider col)
	{
		if (col.tag.Equals ("Obstacle")) {
			Spawner.Instance.ForceStop ();
			BirdInput.Instance.ForceStop ();
			SceneryMovement.Instance.ForceStop ();
			GetComponent<AudioSource> ().Play ();
			if (ResurrectionGameCtrl.Instance) {
				ResurrectionHUD.Instance.StartFade (0.2f, 2);
			} else {
				HUDController.Instance.StartFade (0.2f, 3);
			}
		} else if (col.tag == "Collectable") {
			col.gameObject.GetComponent<AudioSource> ().Play ();
			for (int i = 0; i < col.transform.childCount; i++) {
				col.transform.GetChild (i).gameObject.SetActive (false);
			}
			GameController.Instance.AddScore (1);
			Destroy (col.gameObject, 0.5f);
		}
	}
}
