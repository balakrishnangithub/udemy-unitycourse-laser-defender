using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
	public Texture2D mouseIcon;
	static string sceneWaitingToLoad;

	void Start ()
	{
		Cursor.SetCursor (mouseIcon, new Vector2 (mouseIcon.width / 2, mouseIcon.height / 2), CursorMode.ForceSoftware);
	}

	public void LoadLevel (string sceneName)
	{
		if (sceneName == "Game")
			ScoreKeeper.isGameOver = false;
		SceneManager.LoadScene (sceneName);
	}

	public void LoadLevel (string sceneName, float delayTime)
	{
		sceneWaitingToLoad = sceneName;
		Invoke ("LoadWithDelay", delayTime);
	}

	public void LoadWithDelay ()
	{
		SceneManager.LoadScene (sceneWaitingToLoad);
	}

	public void LoadNextLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void QuitGame ()
	{
		Application.Quit ();
	}
}
