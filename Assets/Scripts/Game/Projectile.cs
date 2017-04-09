using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

	public float damage = 100f;
	public string owner;

	public float GetDamage ()
	{
		return damage;
	}

	public void Hit ()
	{
		Destroy (gameObject);
	}
}
