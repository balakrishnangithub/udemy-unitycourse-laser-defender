using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Text finalScore = GetComponent<Text> ();
		finalScore.text = ScoreKeeper.score.ToString ();
	}
}
