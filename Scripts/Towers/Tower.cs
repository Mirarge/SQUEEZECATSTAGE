using Godot;
using System;

public partial class Tower : Node2D
{
	public int HP;
	public int fireCooldown = 0;
	public int baseCooldown = 10;
	public virtual int sightDistance { get; set; } = 0;
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

	public virtual void TakeDamage(int damage)
	{
		HP -= damage;
	}

	public virtual void SetSightLine()
	{
		GD.Print("Setting horizontal scale to " + sightDistance);
		this.sightArea.Scale = new Vector2(sightDistance+1, this.sightArea.Scale.Y);
		this.sightArea.Position = new Vector2(this.sightArea.Position.X+sightDistance*32, this.sightArea.Position.Y);
	}

	public virtual bool DoISeeAnEnemy()
	{
		return false;
	}
}
