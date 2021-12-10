using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	private string currentLevel;
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
		LoadScene("UI");
	}

	public void LoadLevel(string name)
	{
		if (currentLevel != null)
			UnloadLevel();

		currentLevel = name;
		LoadScene(name);
	}

	public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive)
	{
		print("Loading scene: " + sceneName + " with mode: " + mode);
		SceneManager.LoadSceneAsync(sceneName, mode);
	}

	public void LoadScenes(string[] sceneNames)
	{
		foreach(string scene in sceneNames)
		{
			LoadScene(scene);
		}
	}

	public void UnloadLevel()
	{
		if (currentLevel == null)
			return;

		SceneManager.UnloadSceneAsync(currentLevel);
		currentLevel = null;
	}
}
