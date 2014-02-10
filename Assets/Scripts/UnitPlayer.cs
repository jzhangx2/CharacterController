using UnityEngine;
using System.Collections;

public class UnitPlayer : Unit {

	// Variable to rotate Camera Left and Right
	float cameraRotX = 0f;

	// Variable to restrain how far up/down player can look
	protected float cameraPitchMax = 60f;
	public double jumpLimit = 2;
	protected double jumpCount = 0;

	// Use this for initialization
	public override void Start () {
	
		base.Start ();
	}
	
	// Update is called once per frame
	public override void Update () {

		// Rotation Left and Right
		transform.Rotate (0f, Input.GetAxis ("Mouse X") * turnSpeed * Time.deltaTime, 0f);

		// Rotation Up and Down
		cameraRotX -= Input.GetAxis ("Mouse Y");
		// Restrict up and down movment
		cameraRotX = Mathf.Clamp (cameraRotX, -cameraPitchMax, cameraPitchMax);


		Camera.main.transform.forward = transform.forward;
		Camera.main.transform.Rotate (cameraRotX, 0f, 0f);


		// Movement
		move = new Vector3(Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));
		move.Normalize ();
		move = transform.TransformDirection (move);


		// Jumping
		if (Input.GetKey (KeyCode.Space) && control.isGrounded) {
			jump = true;
			jumpCount++;
		}
		// Double Jump
		if (Input.GetKeyDown (KeyCode.Space) && !control.isGrounded && jumpCount < jumpLimit) {
			jump = true;
			jumpCount++;
		}
		// Reset counter;
		if (control.isGrounded && jumpCount >= jumpLimit)
			jumpCount = 0;

		running = Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift);

		base.Update ();
	}
}
