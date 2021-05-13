using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	public static MusicManager Instance;

	public AudioSource musicPrefab;
	public AudioClip light;
	public AudioClip dark;
	public Tone tone = Tone.Light;
	public float trackFadeTime = 6f;

	private AudioSource currentSource;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			StartMusic();
			DontDestroyOnLoad(this);
		} else {
			Instance.SwitchMusicTone(tone);
			Destroy(gameObject);
		}
	}

	public void SwitchMusicTone(Tone newTone) {
		if (newTone == tone) return;

		tone = newTone;
		float playbackPosition = currentSource.time;
		StartCoroutine(FadeOutAndStop(currentSource, trackFadeTime));

		currentSource = Instantiate(musicPrefab);
		currentSource.transform.parent = transform;
		currentSource.clip = GetAudioClip();
		currentSource.gameObject.name =currentSource.clip.name;

		currentSource.time = playbackPosition;
		StartCoroutine(FadeIn(currentSource, trackFadeTime));
		currentSource.Play();
	}

	private void StartMusic () {
		currentSource = Instantiate(musicPrefab);
		currentSource.transform.parent = transform;
		currentSource.clip = GetAudioClip();
		currentSource.gameObject.name =currentSource.clip.name;
		currentSource.Play();
	}

	private AudioClip GetAudioClip() {
		switch(tone) {
			case Tone.Dark:
				return dark;
			default:
				return light;
		}
	}

	IEnumerator FadeIn(AudioSource source, float fadeTime) {
		float startTime = Time.unscaledTime;
		float currentTime = 0f;

		source.volume = 0f;

		while (startTime + fadeTime > Time.unscaledTime) {
			currentTime = Time.unscaledTime - startTime;

			source.volume = Mathf.Lerp(0f, 1f, currentTime / fadeTime);
			yield return null;
		}

		source.volume = 1f;
	}

	IEnumerator FadeOutAndStop(AudioSource source, float fadeTime) {
		float startTime = Time.unscaledTime;
		float currentTime = 0f;

		source.volume = 1f;

		while (startTime + fadeTime > Time.unscaledTime) {
			currentTime = Time.unscaledTime - startTime;

			source.volume = Mathf.Lerp(1f, 0f, currentTime / fadeTime);
			yield return null;
		}

		source.Stop();
		Destroy(source.gameObject);
	}

	public enum Tone {
		Light,
		Dark
	}
}


#if UNITY_EDITOR
[CustomEditor(typeof(MusicManager))]
[CanEditMultipleObjects]
public class MusicManagerEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		if (!EditorApplication.isPlaying) return;

		if (GUILayout.Button("Play Village")) {
			(target as MusicManager).SwitchMusicTone(MusicManager.Tone.Light);
		}

		if (GUILayout.Button("Play Omnious")) {
			(target as MusicManager).SwitchMusicTone(MusicManager.Tone.Dark);
		}
	}
}
#endif