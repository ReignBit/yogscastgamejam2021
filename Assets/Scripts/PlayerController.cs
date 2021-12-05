using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Tilemap groundMap;
	[SerializeField] private Tilemap collisionsMap;
	[SerializeField] private Tilemap highlightMap;
	[SerializeField] private Tile highlightTile;
	[SerializeField] private float moveSpeed;
	private Vector3Int oldPosition;
	private Camera mainCamera;

	private PlayerMovement controls;

	private void Awake() {
		controls = new PlayerMovement();
		mainCamera = Camera.main;
		oldPosition = Vector3Int.RoundToInt(transform.position);
		highlightMap.SetTile(oldPosition, highlightTile);
	}

	private void OnEnable() {
		controls.Enable();
	}

	private void OnDisable() {
		controls.Disable();
	}

    void Start()
    {
		controls.Main.Movement.performed += context => Move(context);;
    }

	private void Move(UnityEngine.InputSystem.InputAction.CallbackContext context) {
		Vector3 direction = context.ReadValue<Vector2>()/2f;

		switch (context.control.displayName)
		{
			case "W":
			case "S":
				direction.x = direction.y*-1f;
				break;
			case "A":
			case "D":
				direction.y = direction.x;
				break;
		}

		if (CanMove(direction))
		{
			transform.position += direction;
			highlightMap.SetTile(oldPosition, null);
			oldPosition = groundMap.WorldToCell(transform.position);
			Debug.Log(transform.position + " " + oldPosition);
			highlightMap.SetTile(oldPosition, highlightTile);
		}
	}

	private bool CanMove(Vector3 direction) {
		Vector3Int gridPosition = groundMap.WorldToCell(transform.position + direction);
		gridPosition.z = 0;
		return (groundMap.HasTile(gridPosition) && !collisionsMap.HasTile(gridPosition));
	}

    void Update()
    {
    }
}
