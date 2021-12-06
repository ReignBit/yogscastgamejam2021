using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private Camera mainCamera;
	private PlayerInput playerInput;

	private InputAction moveAction;

	private void Awake()
	{
		playerInput 	= GetComponent<PlayerInput>();
		moveAction 		= playerInput.actions["Movement"];
		mainCamera 		= Camera.main;
	}

    void Start()
    {
		moveAction.performed += Move;
		TilemapManager.instance.Entities.SetTile(TilemapManager.instance.Entities.WorldToCell(transform.position), TilemapManager.instance.PlayerTile);
    }

	private void Move(InputAction.CallbackContext context)
	{
		Vector3 direction = context.ReadValue<Vector2>()/2f;

		switch (context.control.displayName)
		{
			case "W":
			case "S":
				direction.x = direction.y*-1f;
				direction.y /= 2f;
				break;
			case "A":
			case "D":
				direction.y = direction.x/2f;
				break;
		}

		if (CanMove(direction))
		{
			Vector3 newPos = transform.position + direction;
			TilemapManager.instance.MoveTile(transform.position, newPos, TilemapManager.instance.Entities);
			transform.position = newPos;
			RoundManager.instance.EndPlayerTurn();
		}
	}

	private bool CanMove(Vector3 direction)
	{
		Vector3Int gridPosition = TilemapManager.instance.Ground.WorldToCell(transform.position + direction);
		gridPosition.z = 0;
		return (TilemapManager.instance.Ground.HasTile(gridPosition) && !TilemapManager.instance.Collision.HasTile(gridPosition));
	}

    void Update()
    {
    }
}
