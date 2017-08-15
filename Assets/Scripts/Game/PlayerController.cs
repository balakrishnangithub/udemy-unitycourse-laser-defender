using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public GameObject laserPrefab;
	public float health = 200f;
	private float maxHealth;
	public float laserSpeed;
	public float firingRate;
	public float playerSpeed;
	public Sprite[] damageSprites;
	public AudioClip laserSound;

	private GameObject damageObject;
	private float maxY;
	private float maxX;
	private float spritePivotToEdge;

	void Start ()
	{
		maxHealth = health;
		SpriteRenderer sr = this.GetComponent<SpriteRenderer> ();
		// Square Sprite, Pivot Center
		spritePivotToEdge = sr.sprite.pivot.x / sr.sprite.pixelsPerUnit;
		maxX = (Camera.main.orthographicSize * Camera.main.aspect) - spritePivotToEdge;
		damageObject = transform.Find ("Damage").gameObject;
		damageObject.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)) {
			MoveLeft();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			MoveRight();
		}
		if (Input.GetKeyDown (KeyCode.Space))
		{
			StartFireLaser();
		} else if (Input.GetKeyUp (KeyCode.Space))
		{
			StopFireLaser();
		}
	}

	void FireLaser ()
	{

		GameObject laser = Instantiate (laserPrefab, this.transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, laserSpeed);
		AudioSource.PlayClipAtPoint (laserSound, this.transform.position);
	}

	void MoveLeft()
	{
		this.transform.position = new Vector3 (
			Mathf.Clamp (this.transform.position.x - playerSpeed * Time.deltaTime, -maxX, maxX), 
			this.transform.position.y, 
			0
		);
	}

	void MoveRight()
	{
		this.transform.position = new Vector3 (
			Mathf.Clamp (this.transform.position.x + playerSpeed * Time.deltaTime, -maxX, maxX), 
			this.transform.position.y, 
			0
		);
	}

	void StartFireLaser()
	{
		InvokeRepeating ("FireLaser", 0f, firingRate);
	}

	void StopFireLaser()
	{
		CancelInvoke ("FireLaser");
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		ScreenFlasher flash = GameObject.Find ("Screen Flasher").GetComponent<ScreenFlasher> ();
		flash.PlayFlash ();
		Projectile missile = collider.GetComponent<Projectile> ();
		if (missile) {
			missile.Hit ();
			health -= missile.GetDamage ();
			if (health <= 0) {
				Die ();
			} else {
				ShowDamage ();
			}
		}
	}

	void ShowDamage ()
	{
		damageObject.GetComponent<SpriteRenderer> ().enabled = true;
		if (health / maxHealth > 0.8) {
			damageObject.GetComponent<SpriteRenderer> ().sprite = damageSprites [0];
		} else if (health / maxHealth > 0.4) {
			damageObject.GetComponent<SpriteRenderer> ().sprite = damageSprites [1];
		} else {
			damageObject.GetComponent<SpriteRenderer> ().sprite = damageSprites [2];
		}
	}

	void Die ()
	{
		GameObject musicPlayer = GameObject.Find ("Music Player");
		if (musicPlayer)
			musicPlayer.GetComponent<AudioSource> ().Stop ();
		ScoreKeeper.isGameOver = true;
		LevelManager levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager> ();
		levelManager.LoadLevel ("End", 3f);
		Destroy (gameObject);
	}
}
