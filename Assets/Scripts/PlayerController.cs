using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private Camera mainCamera;
	private PlayerInput playerInput;

	private InputAction moveAction;
	private InputAction pauseAction;


	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		moveAction 	= playerInput.actions["Movement"];
		pauseAction = playerInput.actions["Pause"];
		mainCamera 	= Camera.main;
	}

    void Start()
    {
		moveAction.performed += Move;
		pauseAction.performed += Pause;

        RoundManager.instance.onPlayerDeath += OnPlayerDeath;

        TilemapManager.instance.Entities.SetTile(TilemapManager.instance.Entities.WorldToCell(transform.position), TilemapManager.instance.PlayerTile);
    }

    void OnPlayerDeath()
    {
        this.enabled = false;
    }

	private void OnDestroy()
	{
		moveAction.performed -= Move;
		pauseAction.performed -= Pause;
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

		Vector3 newPos = transform.position + direction;
		if (TilemapManager.instance.CanMove(newPos))
		{
			TileBase entity = TilemapManager.instance.GetEntity(newPos);

			if (entity == TilemapManager.instance.EnemyTile)
				RoundManager.instance.HitEnemy(newPos);
			else if (entity == TilemapManager.instance.PresentTile)
				RoundManager.instance.CollectPresent(newPos);

			TilemapManager.instance.MoveTile(transform.position, newPos, TilemapManager.instance.Entities);
			transform.position = newPos;
			RoundManager.instance.EndPlayerTurn();
		}
	}

	private void Pause(InputAction.CallbackContext context)
	{
		UIManager.instance.PauseGame(this);
		this.enabled = false;
	}
}