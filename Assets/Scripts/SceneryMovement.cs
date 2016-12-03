using UnityEngine;
using System.Collections;

public class SceneryMovement : MonoBehaviour {
	public GameObject sceneryPrefab;
	public int numberOfInstances = 3;
	public float animationSpeed = 100.0f;
	public float debugSpaceBetweenInstances = 10.0f;

	GameObject[] sceneryInstances;
	float position;
	Bounds sceneryBounds;

	private static SceneryMovement instance = null;

	bool forceStop = false;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
	}

	public void ForceStop() {
		forceStop = true;
	}

	Bounds GetBoundsOfHierarchy( GameObject obj ) {
		var bbox = new Bounds();

		if (obj.GetComponent<Renderer> ()) {
			bbox = obj.GetComponent<Renderer> ().bounds;
		}

		foreach(Transform child in obj.transform)
		{
			var r = GetBoundsOfHierarchy(child.gameObject);
			bbox.Encapsulate(r.min);
			bbox.Encapsulate(r.max);
		}
		return bbox;
	}

	// Use this for initialization
	void Start () {
		sceneryInstances = new GameObject[numberOfInstances];

		for (int i = 0; i < sceneryInstances.Length; ++i) {
			sceneryInstances [i] = (GameObject)Instantiate (sceneryPrefab);
		}

		sceneryBounds = GetBoundsOfHierarchy (sceneryInstances [0]);

//		sceneryInstances [1].transform.eulerAngles = new Vector3 (0, 0, 45);
	}
	
	// Update is called once per frame
	void Update () {
		if (forceStop) {
			return;
		}
		position -= Time.deltaTime * animationSpeed;

		float w = sceneryBounds.size.z + debugSpaceBetweenInstances;
		float k = 1.0f; // how far it goes in the screen

		for (int i = 0; i < sceneryInstances.Length; ++i) {
			float p = w * i + position;
			p = Mathf.Repeat (p + w * k, w * sceneryInstances.Length) - w * k;
			sceneryInstances [i].transform.position = transform.position + new Vector3(0, 0, p);
		}
	}

	public static SceneryMovement Instance {
		get{ return instance; }
	}
}
