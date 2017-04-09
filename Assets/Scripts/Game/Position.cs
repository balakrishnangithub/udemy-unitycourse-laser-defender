using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour
{
	public bool isRestricted = false;

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere (this.transform.position, 0.5f);
	}
}
