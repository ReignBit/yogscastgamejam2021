using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Slider[] volumeSliders;

	[SerializeField] private TMP_Text[] volumeTexts;
	private string[] volumeTypes;
	public static UIManager instance;
	private GameObject previous;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("OH NO");
		}
		else
		{
			instance = this;
		}
	}


	public void Start()
	{
		volumeTypes = new string[volumeTexts.Length];
		for (int i = 0; i < volumeTexts.Length; i++)
		{
			volumeTypes[i] = volumeSliders[i].name;
			float volume = PlayerPrefs.HasKey(volumeTypes[i]) ? PlayerPrefs.GetFloat(volumeTypes[i]) : 0.5f;
			PlayerPrefs.SetFloat(volumeTypes[i], volume);
			volumeTypes[i] 			= volumeSliders[i].name;
			volumeTexts[i].text 	= volume.ToString("#%");
			volumeSliders[i].value 	= volume;
		}

		Resources.FindObjectsOfTypeAll<Canvas>()[0].transform.Find("Menu").gameObject.SetActive(true);
		AudioManager.instance.PlayMenuMusic();
	}

	public void SetVolume(Slider slider)
	{
		if (!PlayerPrefs.HasKey(slider.name))
		{
			print(slider.name + " is not a recognized sound setting.");
			return;
		}

		AudioListener.volume = slider.value;
		volumeTexts[System.Array.IndexOf(volumeTypes, slider.name)].text = slider.value.ToString("#%");
		PlayerPrefs.SetFloat(slider.name, AudioListener.volume);

		AudioManager.instance.AdjustVolume();
		PlayerPrefs.Save();
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SelectLevel(string name)
	{
		SceneLoader.instance.LoadScene(name);
	}

	public void SwitchToOptions(GameObject previous)
	{
		this.previous = previous;
	}

	public void SwitchBackFromOptions()
	{
		previous.SetActive(true);
	}
}
