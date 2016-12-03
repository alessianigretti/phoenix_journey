using UnityEngine;
using System.Collections;

public class SunController : MonoBehaviour {

	public float howBrightItIs = 400.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (ResurrectionGameCtrl.Instance) {
			Light l = GetComponent<Light> ();

			if (l) {
				l.range = ResurrectionGameCtrl.Instance.sunT * howBrightItIs;
			}
		}

	}
}
