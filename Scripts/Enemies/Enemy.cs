using Godot;
using System;

public partial class Enemy : Node2D
{
	public int HP;
	public int attackCooldown = 0;
	public int baseCooldown = 10;
	public float speed = 0.5f;
	public virtual int sightDistance { get; set; } = 0;
	public Area2D sightArea;

	public override void _Ready()
	{
		base._Ready();
		this.sightArea = GetNode<Area2D>("SightArea");
		SetSightLine();
	}
	public virtual void Attack()
	{

	}

	public virtual void TakeDamage(int damage)
	{
		HP -= damage;
	}

	public virtual void MoveOnce()
	{
		Position += new Vector2(-speed, 0);
	}

	public virtual void SetSightLine()
	{
		this.sightArea.Scale = new Vector2(sightDistance + 1, this.sightArea.Scale.Y);
		this.sightArea.Position = new Vector2(this.sightArea.Position.X - sightDistance * 32, this.sightArea.Position.Y);
	}
}
