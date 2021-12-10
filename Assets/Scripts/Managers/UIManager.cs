using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Slider[] volumeSliders;

	[SerializeField] private TMP_Text[] volumeTexts;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject options;
	[SerializeField] private GameObject HUD;
	[SerializeField] private GameObject levelSelect;
	private string[] volumeTypes;
	public static UIManager instance;
	private GameObject previous;
	private PlayerController player;


	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("OH NO");
		}
		else
		{
			instance = this;
			mainMenu.SetActive(true);
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

	public void PauseGame(PlayerController player)
	{
		this.player = player;
		pauseMenu.SetActive(true);
		HUD.SetActive(false);
	}

	public void ContinueGame()
	{
		pauseMenu.SetActive(false);
		player.enabled = true;
		HUD.SetActive(true);
	}

	public void SelectLevel(string name)
	{
		levelSelect.SetActive(false);
		SceneLoader.instance.LoadScene(name);
		HUD.SetActive(true);
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
