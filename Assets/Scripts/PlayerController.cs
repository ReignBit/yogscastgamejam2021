using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Tile highlightTile;
	private TilemapManager tilemapManager;
	private Vector3Int oldPosition;
	private Camera mainCamera;

	private PlayerMovement controls;

	private void Awake()
	{
		tilemapManager = TilemapManager.instance;
		controls = new PlayerMovement();
		mainCamera = Camera.main;
		oldPosition = Vector3Int.RoundToInt(transform.position);
		tilemapManager.Highlights.SetTile(oldPosition, highlightTile);
	}

	private void OnEnable()
	{
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}

    void Start()
    {
		controls.Main.Movement.performed += context => Move(context);;
    }

	private void Move(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
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
			tilemapManager.Highlights.SetTile(oldPosition, null);
			oldPosition = tilemapManager.Ground.WorldToCell(transform.position);
			Debug.Log(transform.position + " " + oldPosition);
			tilemapManager.Highlights.SetTile(oldPosition, highlightTile);
		}
	}

	private bool CanMove(Vector3 direction)
	{
		Vector3Int gridPosition = tilemapManager.Ground.WorldToCell(transform.position + direction);
		gridPosition.z = 0;
		return (tilemapManager.Ground.HasTile(gridPosition) && !tilemapManager.Collision.HasTile(gridPosition));
	}

    void Update()
    {
    }
}
