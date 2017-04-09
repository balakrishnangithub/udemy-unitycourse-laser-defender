using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	private static MusicPlayer instance = null;

	public AudioClip startMusic;
	public AudioClip gameMusic;
	public AudioClip endMusic;

	private AudioSource musicPlayer;

	void Awake ()
	{
		musicPlayer = GetComponent<AudioSource> ();

		if (!instance) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			musicPlayer.Stop ();
			musicPlayer.clip = startMusic;
			musicPlayer.loop = true;
			musicPlayer.Play ();
		} else
			Destroy (gameObject);
	}

	void OnLevelWasLoaded (int level)
	{
		musicPlayer.Stop ();

		switch (level) {
		case 0:
			musicPlayer.clip = startMusic;
			musicPlayer.volume = 0.1f;
			break;
		case 1:
			musicPlayer.clip = gameMusic;
			musicPlayer.volume = 0.03f;
			break;
		case 2:
			musicPlayer.clip = endMusic;
			musicPlayer.volume = 0.1f;
			break;
		default:
			break;
		}
		musicPlayer.loop = true;
		musicPlayer.Play ();
	}
}
