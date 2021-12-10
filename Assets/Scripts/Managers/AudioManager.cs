using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	private static AudioSource audioSource;
	[SerializeField] private List<string> music;
	[SerializeField] private List<string> soundEffects;
	[SerializeField] private string menuMusic;
	private string musicPath;
	private string soundPath;
	private int songIndex;

	private AudioClip nextSong;

    private void Awake()
	{
        if (instance != null)
        {
            Debug.LogError("More than one TilemapManager!");
        }
        else
        {
            instance = this;

			audioSource = GetComponent<AudioSource>();

			musicPath 		= "file://" + Application.streamingAssetsPath + "/Music/";
			soundPath 		= "file://" + Application.streamingAssetsPath + "/Sound/";

			music 			= GetFiles(Application.streamingAssetsPath + "/Music/", ".mp3", new string[] {"Walk_through_the_Snowy.mp3"});
			soundEffects 	= GetFiles(Application.streamingAssetsPath + "/Sounds/", ".mp3");

			songIndex = 0;
        }
	}

	private void Start()
	{
		AdjustVolume();
		if (GameObject.Find("Menu"))
			PlayMenuMusic();
		else
			LoadAudio(music[songIndex], true);
	}

	private List<string> GetFiles(string path, string extension = "*", string[] ignored = null)
	{
		List<string> filteredFiles = new List<string>();
		string[] files;
		files = Directory.GetFiles(path);

		if (extension == "*")
		{
			filteredFiles.AddRange(files);
		}
		else
		{
			foreach (string file in files)
			{
				if (file.EndsWith(extension))
					filteredFiles.Add(file);
			}
		}

		if (ignored != null)
		{
			filteredFiles.RemoveAll(filePath => {
				string[] splits = filePath.Split('/');
				string file = splits[splits.Length - 1];

				return ignored.Contains(file);
			});
		}

		return filteredFiles;
	}

	public void AdjustVolume()
	{
		audioSource.volume = PlayerPrefs.GetFloat("Main Volume") * PlayerPrefs.GetFloat("Music Volume");
	}

	public void PlayMenuMusic()
	{
		StartCoroutine(LoadAudio(musicPath+menuMusic+".mp3", true, AudioType.MPEG, true));
	}

	private IEnumerator LoadAudio(string uri, bool startPlaying = false, AudioType audioType = AudioType.MPEG, bool loop = false)
	{
		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, audioType))
		{
			yield return www.SendWebRequest();

			switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(www.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(www.error);
                    break;
                case UnityWebRequest.Result.Success:
					if (startPlaying)
					{
						print("Song loaded. Playing now");
						PlaySong(DownloadHandlerAudioClip.GetContent(www), loop);
					}
					else
					{
						print("Finished loading next song");
						nextSong = DownloadHandlerAudioClip.GetContent(www);
					}
				break;
            }
		}
	}

	private void PlaySong(AudioClip audioClip, bool loop = false)
	{
		audioSource.clip = audioClip;
		audioSource.Play();

		if (music.Count > 0)
			songIndex = ++songIndex % music.Count;

		audioSource.loop = loop;

		if (!loop)
		{
			StartCoroutine(LoadAudio(music[songIndex]));
			Invoke("PlayNext", audioClip.length+5);
		}
	}

	private void PlayNext()
	{
		PlaySong(nextSong);
	}

}
