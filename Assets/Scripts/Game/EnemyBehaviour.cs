using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
	
	public ScoreKeeper scoreKeeper;
	public GameObject laserPrefab;
	public float health = 150f;
	public int killPoint = 10;
	public float laserSpeed;
	[Range (0.001f, 0.06f)] public float firingRate;

	public AudioClip laserSound;
	public AudioClip destroyedSound;

	void Start ()
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	void Update ()
	{
		if (!ScoreKeeper.isGameOver && Random.value < firingRate)
			FireLaser ();
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.GetComponent<Projectile> ();
		if (missile) {
			missile.Hit ();
			health -= missile.GetDamage ();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die ()
	{
		AudioSource.PlayClipAtPoint (destroyedSound, this.transform.position);
		Destroy (gameObject);
		scoreKeeper.Score (killPoint);		
	}

	void FireLaser ()
	{
		GameObject laser = Instantiate (laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, -laserSpeed);
		AudioSource.PlayClipAtPoint (laserSound, this.transform.position);
	}
}
