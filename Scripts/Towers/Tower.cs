using Godot;
using System;

public partial class Tower : Node2D
{
	public int HP;
	public int fireCooldown = 0;
	public int baseCooldown = 10;
	public int sightDistance = 0;
	public Area2D sightArea;

	public override void _Ready()
	{
		base._Ready();
		this.sightArea = GetNode<Area2D>("SightArea");
		SetSightLine();
	}

	public virtual void Fire()
	{
		
	}

	public virtual void TakeDamage()
	{

	}

	public virtual void SetSightLine()
	{
		GD.Print("Setting horizontal scale to " + sightDistance);
		this.sightArea.Scale = new Vector2(sightDistance+1, this.sightArea.Scale.Y);
	}
}
