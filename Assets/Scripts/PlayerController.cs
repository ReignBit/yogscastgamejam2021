using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Tilemap groundMap;
	[SerializeField] private Tilemap collisionsMap;
	[SerializeField] private float moveSpeed;
	private Camera camera;

	private MouseInput mouseInput;
	private PlayerMovement controls;
	private Vector3 destination;

	private void Awake() {
		mouseInput = new MouseInput();
		controls = new PlayerMovement();
		camera = Camera.main;
	}

	private void OnEnable() {
		mouseInput.Enable();
		controls.Enable();
	}

	private void OnDisable() {
		mouseInput.Disable();
		controls.Disable();
	}

    // Start is called before the first frame update
    void Start()
    {
		destination = transform.position;
		mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
		controls.Main.Movement.performed += context => Move(context.ReadValue<Vector2>());

    }

	private void MouseClick() {
		Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		Vector3Int gridPosition = groundMap.WorldToCell(mousePosition);
		if (groundMap.HasTile(gridPosition))
			destination = mousePosition;
	}

	private void Move(Vector2 direction) {
		Debug.Log(transform.position + (Vector3)direction);
		if (CanMove(direction))
			transform.position += (Vector3)direction;
	}

	private bool CanMove(Vector2 direction) {
		Vector2 position = camera.ScreenToWorldPoint(direction);
		Vector3Int gridPosition = groundMap.WorldToCell(position + direction);
		return groundMap.HasTile(gridPosition) && !collisionsMap.HasTile(gridPosition);
	}

    // Update is called once per frame
    void Update()
    {
		if (Vector3.Distance(transform.position, destination) > 0.1f)
			transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
    }
}
