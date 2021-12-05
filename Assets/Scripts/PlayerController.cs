using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Tilemap groundMap;
	[SerializeField] private Tilemap collisionsMap;
	[SerializeField] private float moveSpeed;
	private Camera mainCamera;

	private PlayerMovement controls;

	private void Awake() {
		controls = new PlayerMovement();
		mainCamera = Camera.main;
	}

	private void OnEnable() {
		controls.Enable();
	}

	private void OnDisable() {
		controls.Disable();
	}

    void Start()
    {
		controls.Main.Movement.performed += context => Move(context.ReadValue<Vector2>()/2);
    }

	private void Move(Vector2 direction) {
		Debug.Log(direction);
		if (CanMove(direction))
			transform.position += (Vector3)direction;
	}

	private bool CanMove(Vector2 direction) {
		Vector2 position = mainCamera.ScreenToWorldPoint(direction);
		Vector3Int gridPosition = groundMap.WorldToCell(position + direction);
		return true;
	}

    void Update()
    {
    }
}
