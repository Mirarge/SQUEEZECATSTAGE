using Godot;
using System;

public partial class Tower : Node2D
{
	public TowerManager manager;

	public virtual int HP { get; set; } = 1;
	public virtual int fireCooldown { get; set; } = 0;
	public virtual int baseCooldown { get; set; } = 10;
	public virtual int sightDistance { get; set; } = 0;
	public Area2D sightArea;
	public Sprite2D sprite;
	public Tile myTile;

	private int enemiesInView = 0;

	public override void _Ready()
	{
		base._Ready();
		this.sightArea = GetNode<Area2D>("SightArea");
		this.sprite = GetNode<Sprite2D>("Sprite2D");
		this.sightArea.Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
		this.sightArea.Connect("area_exited", new Callable(this, nameof(OnAreaExited)));
		SetSightLine();
	}

	public async virtual void Fire()
	{
		
	}

	public virtual void TakeDamage(int damage)
	{
		HP -= damage;
		if (HP <= 0) { 
			this.manager.DestroyTower(this);
		}
	}

	public virtual void SetSightLine()
	{
		this.sightArea.Scale = new Vector2(sightDistance+1, this.sightArea.Scale.Y);
		this.sightArea.Position = new Vector2(this.sightArea.Position.X+sightDistance*32, this.sightArea.Position.Y);
	}
	private void OnAreaEntered(Area2D area){
		enemiesInView++;
	}
	private void OnAreaExited(Area2D area) {
		enemiesInView--; 
	}
	public virtual bool DoISeeAnEnemy()
	{
		return enemiesInView > 0;
	}
}
