using UnityEngine;
using System.Collections;

public class BirdInput : MonoBehaviour {

	public float howWideItGoesOnXAxis = 5;
	public float howWideItGoesOnYAxis = 1;
	public float howWideItGoesOnZAngle = 60.0f;

	Vector3 initialPosition;
	Vector3 initialRotationInEulerAngles;
	bool forceStop = false;

	Vector3 initialMouseTouchPosition;
	bool wasTouched = false;
	float currentXKoef = 0.0f;

	private static BirdInput instance = null;

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
	void Start () {
		initialPosition = transform.position;
		initialRotationInEulerAngles = transform.eulerAngles;
	}

	Vector3 GetMouseInCube() {
		var mousePos = Input.mousePosition;
		mousePos.x = mousePos.x / Screen.width - 0.5f;
		mousePos.y = mousePos.y / Screen.height - 0.5f;
		return mousePos;
	}

	// Update is called once per frame
	void Update () {
		if (forceStop) {
			return;
		}
		if (Input.GetMouseButton (0)) {
			if (wasTouched == false) {
				initialMouseTouchPosition = GetMouseInCube ();
				wasTouched = true;
			}

			var mousePos = GetMouseInCube () - initialMouseTouchPosition;
			initialMouseTouchPosition = GetMouseInCube ();

			currentXKoef += mousePos.x;
			currentXKoef = Mathf.Clamp (currentXKoef, -0.67f, 0.67f);

			transform.position = new Vector3 (
				initialPosition.x + currentXKoef * howWideItGoesOnXAxis,
				initialPosition.y + Mathf.Abs (mousePos.x) * howWideItGoesOnYAxis,
				initialPosition.z);

			transform.eulerAngles = new Vector3 (initialRotationInEulerAngles.x, initialRotationInEulerAngles.y, initialRotationInEulerAngles.z + currentXKoef * howWideItGoesOnZAngle);
		} else {
			wasTouched = false;
		}
	}

	public static BirdInput Instance {
		get{ return instance; }
	}

	public void ForceStop() {
		forceStop = true;
	}
}
