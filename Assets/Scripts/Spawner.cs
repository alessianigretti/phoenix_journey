using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public Vector2 spawningTimeIntervalInSeconds = new Vector2(1.0f, 2.0f);
	public GameObject[] obstaclePrefabs;
	public GameObject tokenPrefab;

	public float distanceBetweenTokens = 3.0f;

	public int[] tokenSequences = new int[] {8, 12, 16};
	public int howManyObstaclesToSpawnInARow = 2;

	float timeToNextSpawn = 0.0f;
	int obstacleSpawnCount = 0;
	bool forceStop = false;
	List<GameObject> generatedObstacles = new List<GameObject> ();

	public bool resurrectionScene = false;

	private static Spawner instance = null;

	void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
	}

	void NewTimeToNextSpawn() {
		timeToNextSpawn = Random.Range (spawningTimeIntervalInSeconds.x, spawningTimeIntervalInSeconds.y);
	}

	Vector3 randomPosInBounds() {
		Vector3 pos = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		pos = transform.TransformPoint(pos * 0.5f);
		return pos;
	}

	void SetSpeed(GameObject obj) {
		if (!resurrectionScene) {
			return;
		}

		ItemsMovement itemsMovement = obj.GetComponent<ItemsMovement>();
		if (itemsMovement) {
			itemsMovement.speedInPositionPerSecond -= 100.0f;
		}
	}

	void SpawnObstacle() {
		if (obstaclePrefabs.Length > 0) {
			GameObject go = (GameObject) Instantiate (obstaclePrefabs [ Random.Range(0, obstaclePrefabs.Length) ], randomPosInBounds(), transform.rotation);

			SetSpeed (go);

			generatedObstacles.Add (go);
		} else {
			Debug.Log ("please set obstacle prefabs to " + name);
		}
	}

	void SpawnTokens() {
		int count = tokenSequences [Random.Range (0, tokenSequences.Length)];

		Vector3 pos = randomPosInBounds ();
//		float direction = Random.value < 0.5f ? 1.0f : -1.0f;
		float direction = pos.x < 0.0f ? 1.0f : -1.0f;
		for (int i = 0; i < count; ++i) {
			Vector3 pos2 = pos + new Vector3 (direction * Mathf.Sin(i / 4.0f) * 3.0f * (i / (float)count), 0.0f, i * distanceBetweenTokens);
			GameObject token = (GameObject) Instantiate (tokenPrefab, pos2, transform.rotation);

			SetSpeed (token);
		}
	}

	// Use this for initialization
	void Start () {
		NewTimeToNextSpawn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (forceStop) {
			return;
		}
		if (timeToNextSpawn > 0.0f)
			timeToNextSpawn -= Time.deltaTime;
		else {
			NewTimeToNextSpawn ();
			if (howManyObstaclesToSpawnInARow < 0 || obstacleSpawnCount < howManyObstaclesToSpawnInARow) {
				obstacleSpawnCount += 1;
				SpawnObstacle ();
			} else {
				SpawnTokens ();
				obstacleSpawnCount = 0;
			}
		}
	}

	public static Spawner Instance {
		get { return instance; }
	}

	public void ForceStop() {
		forceStop = true;
		foreach (GameObject obstacle in generatedObstacles) {
			obstacle.GetComponent<ItemsMovement> ().ForceStop ();
		}
	}

	public void RemoveObstacle(GameObject obstacle){
		generatedObstacles.Remove (obstacle);
	}
}
