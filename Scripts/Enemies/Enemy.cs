using Godot;
using System;

public partial class Enemy : Node2D
{
	public int HP;
	public float speed = 0.5f;
	public int attackStrength = 1;
	public string name;
	public int SightDistance { get { return sightDistance; } set { sightDistance = value; if (this.sightArea != null) { SetSightLine(); }; } }
	private int sightDistance = 0;
	public int attackCooldown = 50;
	public int baseCooldown = 50;
	public WaveManager manager;
	
	public Area2D sightArea;
	public Sprite2D sprite;

	protected Tower towerInSight = null;

	public int budgetCost = 1;
	private bool isSpriteFlipped = false;

	public override void _Ready()
	{
		base._Ready();
		this.sightArea = GetNode<Area2D>("SightArea");
		this.sprite = GetNode<Sprite2D>("Sprite2D");
		this.sightArea.Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
		this.sightArea.Connect("area_exited", new Callable(this, nameof(OnAreaExited)));
		SetSightLine();
	}
	public virtual void Attack()
	{
		if (attackCooldown > 0) attackCooldown--;
		else
		{
			this.sprite.FlipH = !this.sprite.FlipH;
			isSpriteFlipped = !isSpriteFlipped;
			attackCooldown = baseCooldown;
			towerInSight.TakeDamage(attackStrength);
		}
	}

	public virtual void TakeDamage(int damage)
	{
		HP -= damage;
		if (HP <= 0)
		{
			this.manager.DestroyEnemy(this);
		}
	}

	public virtual void Update()
	{
		if (DoISeeATower())
		{
			Attack();
		}
		else
		{
			Move();
		}
			
	}

	public virtual void Move()
	{
		Position += new Vector2(-speed, 0);
        if (Position.X < manager.gameManager.Lboundary.Position.X)
        { //If we're too far out, just remove the enemy.
            HP = -1;
            this.manager.DestroyEnemy(this);
        }
    }

	public virtual void SetSightLine()
	{
		this.sightArea.Scale = new Vector2(SightDistance + 1, this.sightArea.Scale.Y);
		this.sightArea.Position = new Vector2(this.sightArea.Position.X - SightDistance * 32, this.sightArea.Position.Y);
	}

	private void OnAreaEntered(Area2D area) {
		var parent = area.GetParent();
		if (parent is Tower tower && towerInSight == null)
		{
			towerInSight = tower;
		}
	}
	private void OnAreaExited(Area2D area) {
		var parent = area.GetParent();
		if (parent == towerInSight)
		{
			towerInSight = null;
			if (isSpriteFlipped)
			{
				isSpriteFlipped = false;
				sprite.FlipH = false;
			}
		}
	}
	public virtual bool DoISeeATower()
	{
		return towerInSight != null;
	}
}
