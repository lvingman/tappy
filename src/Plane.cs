using Godot;
using System;
using Tappy.enums;

public partial class Plane : CharacterBody2D
{
	private const float JUMP_FORCE = -900.0f;
	private const float GRAVITY = 2000.0f;
	private const float ROTATION_JUMP = 315.0f;
	private const float ROTATION_FALL_LIMIT = 90.0f;
	
	private float objDelta;
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		objDelta = (float)delta;
		ApplyGravity();
		ProcessPlayerInput();
	}

	private void ProcessPlayerInput()
	{
		if (Input.IsActionJustPressed(inputs.FLY))
		{
			Jump();
		}
	}

	private void ApplyGravity()
	{
		Vector2 velocity = Velocity;
		velocity.Y += GRAVITY * objDelta;
		Velocity = velocity;
		MoveAndSlide();
		if (ShouldPlaneRotateOnFall())
		{
			Rotation += 0.095f;
		}
	}

	private void Jump()
	{
		Vector2 velocity = Velocity;
		velocity.Y = JUMP_FORCE;
		Velocity = velocity;
		MoveAndSlide();
		Rotation = float.DegreesToRadians(ROTATION_JUMP);
	}

	private bool ShouldPlaneRotateOnFall()
	{
		bool hasPlaneReachedRotationLimit = double.RadiansToDegrees(Rotation) >= 90;
		bool isPlaneFalling = Velocity.Y > 0;
		return (!hasPlaneReachedRotationLimit && isPlaneFalling);
	}
}
