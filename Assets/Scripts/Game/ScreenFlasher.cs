using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlasher : MonoBehaviour
{

	public float startAlpha = 1;
	private CanvasGroup canvasGroup;
	private bool flash = false;

	void Start ()
	{
		canvasGroup = GetComponent<CanvasGroup> ();
	}

	void Update ()
	{
		if (flash) {
			canvasGroup.alpha = canvasGroup.alpha - Time.deltaTime;
			if (canvasGroup.alpha <= 0) {
				canvasGroup.alpha = 0;
				flash = false;
			}
		}
	}

	public void PlayFlash ()
	{
		flash = true;
		canvasGroup.alpha = startAlpha;
	}
}
