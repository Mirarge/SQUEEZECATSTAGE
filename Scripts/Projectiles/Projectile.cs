using Godot;
using System;

public partial class Projectile : Node2D
{
    public string name;
    public ProjectileManager manager;
    public bool isTowerProjectile;
    public Vector2 direction;
    public Area2D hitbox;
    public int damage = 1;
    public int HP = 1; //Not making a property out of this since there might be moments where HP is higher than the baseHP, such as neighbouring towers having a special effect
    public int baseHP = 1;

    public override void _Ready()
    {
        base._Ready();
        this.hitbox = GetNode<Area2D>("HitboxArea");
        this.hitbox.Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
    }
    public void ResetHP()
    {
        HP = baseHP;
    }
    public void Update()
    {
        Position += direction;
        if (Position.X > manager.gameManager.boundary.Position.X)
        { //If we're too far out, just remove the projectile.
            HP = -1;
            this.manager.RequestProjectileRemoval(this);
        }
        
    }
    public void UpdateCollisionMask()
    {
        if (isTowerProjectile) //Making sure the projectile only detects the hitbox of the opposing team
        {
            hitbox.SetCollisionMaskValue(1, false);
            hitbox.SetCollisionMaskValue(3, true);
        }
        else
        {
            hitbox.SetCollisionMaskValue(1, true);
            hitbox.SetCollisionMaskValue(3, false);
        }
    }

    private void OnAreaEntered(Area2D area)
    {
        //Should only detect the opposing team because of collision layers
        var parent = area.GetParent();
        if (HP > 0)
        {
            if (isTowerProjectile)
            {
                (parent as Enemy).TakeDamage(damage);
            }
            else
            {
                (parent as Tower).TakeDamage(damage);
            }
            TakeDamage(1); //Makes piercing projectiles possible if wanted later on.
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP == 0)
        {
            HP = -1; //Hacky fix to make sure "dead" projectiles don't try to destroy themself several times.
            this.manager.RequestProjectileRemoval(this);
        }
    }
}
