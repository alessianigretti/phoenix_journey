using UnityEngine;
using System.Collections;

public class ItemsMovement : MonoBehaviour {

	public float speedInPositionPerSecond = -10.0f;
	public float destroySelfIfPosZisSmallerThanThis = -10.0f;

	Vector3 initialPosition;

	bool forceStop = false;

	public void ForceStop() {
		forceStop = true;
	}

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (forceStop) {
			return;
		}
		float translation = Time.deltaTime * speedInPositionPerSecond;
		transform.Translate(0, 0, translation);

		if (transform.position.z < initialPosition.z + destroySelfIfPosZisSmallerThanThis) {
			Spawner.Instance.RemoveObstacle (this.gameObject);
			Destroy(this.gameObject);
		}
	}
}
