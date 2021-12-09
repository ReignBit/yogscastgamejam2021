using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	private static AudioSource audioSource;
	[SerializeField] private AudioClip[] music;
	[SerializeField] private AudioClip[] soundEffects;
	[SerializeField] private string menuMusic;
	private string musicPath;
	private string soundPath;

    private void Awake()
	{
        if (instance != null)
        {
            Debug.LogError("More than one TilemapManager!");
        }
        else
        {
            instance = this;
        }

		musicPath = "file://" + Application.streamingAssetsPath + "/Music/";
		soundPath = "file://" + Application.streamingAssetsPath + "/Sound/";
		audioSource = GetComponent<AudioSource>();

		StartCoroutine(LoadAudio(musicPath+menuMusic+".mp3"));
	}

	private void Start()
	{
		AdjustVolume();
	}

	public void AdjustVolume()
	{
		audioSource.volume = PlayerPrefs.GetFloat("Main Volume") * PlayerPrefs.GetFloat("Music Volume");
	}

	private IEnumerator LoadAudio(string uri)
	{
		print(uri);
		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.MPEG))
		{
			yield return www.SendWebRequest();

			if (www.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.Log(www.error);
			}
			else
			{
				PlayAudio(DownloadHandlerAudioClip.GetContent(www));
			}
		}
	}

	private void PlayAudio(AudioClip audioClip)
	{
		audioSource.clip = audioClip;
		audioSource.Play();
		audioSource.loop = false;
	}

}
