using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	#region Member Variables
	/// <summary>
	/// Player movement speed
	/// </summary>
	public float movementSpeed;

	/// <summary>
	/// Animation state machine local reference
	/// </summary>
	private Animator animator;

	private Rigidbody2D rb;

	private Vector2 movement;
	#endregion

	// Use this for initialization
	void Start ()
	{
		// get the local reference
		animator = GetComponent<Animator>();

		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get the player input
		GetInput();
		//Update the snimation state
		UpdateAnimator();
	}

    private void FixedUpdate()
    {
		//Move the player
		MovePlayer();
    }

	private void GetInput()
    {
		// check for player exiting the game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
	}

	private void MovePlayer()
    {
		rb.velocity = movement.normalized * movementSpeed;
	}

	private void UpdateAnimator()
    {
		
	}

}
