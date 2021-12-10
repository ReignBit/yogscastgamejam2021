using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("ONLY OOOOOOOOOOONE");
		}
		else
		{
			instance = this;
		}
	}

	private void Start()
	{
		LoadScene("UI", LoadSceneMode.Single);
	}

	public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
	{
		SceneManager.LoadSceneAsync(sceneName, mode);
	}

	public void LoadScenes(string[] sceneNames)
	{
		foreach(string scene in sceneNames)
		{
			LoadScene(scene);
		}
	}
}
