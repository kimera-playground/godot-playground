using Godot;
using System;

public partial class Sprite2D : Godot.Sprite2D
{
	private int _speed = 400;
	private float _angularSpeed = Mathf.Pi;
	
	[Signal]
	public delegate void HealthDepletedEventHandler();

	private int _health = 10;
	
	public void TakeDamage(int amount)
	{
		_health -= amount;

		if (_health <= 0)
		{
			EmitSignal(SignalName.HealthDepleted);
		}
	}

	
	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
	  	timer.Timeout += OnTimerTimeout;
	}

	public override void _Process(double delta)
	{
		var direction = 0;
		if (Input.IsActionPressed("ui_left"))
		{
			direction = -1;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			direction = 1;
		}

		Rotation += _angularSpeed * direction * (float)delta;

		var velocity = Vector2.Zero;
		if (Input.IsActionPressed("ui_up"))
		{
			velocity = Vector2.Up.Rotated(Rotation) * _speed;
		}

		Position += velocity * (float)delta;
	}

	
	public Sprite2D()
	{
		GD.Print("Hello, world!");
	}
	
	private void OnTimerTimeout()
	{
		Visible = !Visible;
	}

	
	
	private void _on_button_pressed()
	{
	   SetProcess(!IsProcessing());
	}

}


