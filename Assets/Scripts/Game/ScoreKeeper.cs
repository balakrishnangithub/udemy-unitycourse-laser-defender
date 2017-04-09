using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{

	public static int score = 0;
	public static bool isGameOver = false;
	private Text scoreText;

	void Start ()
	{
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		Reset ();
		Score (score);
	}

	public void Score (int point)
	{
		score += point;
		scoreText.text = score.ToString ();
	}

	public static void Reset ()
	{
		score = 0;
	}
}
