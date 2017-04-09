using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

	public GameObject enemyPrefab;
	public Sprite[] enemySprites;
	public int currentEnemyIndex = -1;
	public float width;
	public float height;
	public float moveSpeed;
	public float spawnDelay = 0.5f;
	private bool isMovingLeft = true;
	private float camSizeY;
	// Use this for initialization
	void Start ()
	{
		enemySprites = Resources.LoadAll<Sprite> ("Enemies");
		camSizeY = Camera.main.orthographicSize * Camera.main.aspect;
		SpawnEnemy ();
	}

	void SetNextEnemySprite ()
	{
		if (currentEnemyIndex < enemySprites.Length - 1)
			currentEnemyIndex++;
		enemyPrefab.GetComponent<SpriteRenderer> ().sprite = enemySprites [currentEnemyIndex];
	}

	void SpawnEnemy ()
	{
		SetNextEnemySprite ();
		foreach (Transform childPositionGameObject in transform) {
			GameObject enemy = Instantiate (enemyPrefab, childPositionGameObject.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = childPositionGameObject.transform;
		}
	}

	void SpawnEnemyUntilFull ()
	{
		if (!ScoreKeeper.isGameOver) {
			Transform spawnablePositionGameObject = NextAvailablePosition ();
			if (spawnablePositionGameObject) {
				GameObject enemy = Instantiate (
					                   enemyPrefab, spawnablePositionGameObject.position, Quaternion.identity
				                   ) as GameObject;
				spawnablePositionGameObject.GetComponent<Position> ().isRestricted = true;
				enemy.transform.parent = spawnablePositionGameObject;
				if (NextAvailablePosition ())
					Invoke ("SpawnEnemyUntilFull", spawnDelay);
			}
		}
	}

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (width, height));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!ScoreKeeper.isGameOver) {
			if (isMovingLeft) {
				if ((this.transform.position.x - width / 2f) <= -camSizeY)
					isMovingLeft = false;
				this.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
			} else {
				if ((this.transform.position.x + width / 2f) >= camSizeY)
					isMovingLeft = true;
				this.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
			}
			if (AllMembersDead ())
				SpawnEnemyUntilFull ();
		}
	}

	bool AllMembersDead ()
	{
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0)
				return false;
		}
		ResetPositionAvailability ();
		return true;
	}

	void ResetPositionAvailability ()
	{
		SetNextEnemySprite ();
		foreach (Transform childPositionGameObject in transform) {
			childPositionGameObject.GetComponent<Position> ().isRestricted = false;
		}
	}

	Transform NextAvailablePosition ()
	{
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0 &&
			    !childPositionGameObject.GetComponent<Position> ().isRestricted)
				return childPositionGameObject;
		}
		return null;
	}
}
