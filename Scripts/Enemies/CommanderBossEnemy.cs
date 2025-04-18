using Godot;
using System;

public partial class CommanderBossEnemy : Enemy
{
    public CommanderBossEnemy()
    {
        base.HP = 400;
        base.attackStrength = 200;
        base.budgetCost = 24;
        base.speed = 0.2f;
        base.attackCooldown = base.baseCooldown = 300;
        base.SightDistance = 4;
    }

    public override void Attack()
    {
        if (attackCooldown > 0) { attackCooldown--; Move(); }
        else
        {
            attackCooldown = baseCooldown;
            manager.gameManager.projectileManager.SpawnProjectile("BossShockwaveProjectile", Position, new Vector2(-3, 0), this.ZIndex + 1, false);
        }
    }

    public override void Update()
    {
        bool towerInMeleeRange = false;
        if (towerInSight != null)
        {
            towerInMeleeRange = (this.GlobalPosition.DistanceTo(base.towerInSight.GlobalPosition) < 50);
        }
        
        if (towerInMeleeRange)
        {
            base.Attack();
        }
        else if(DoISeeATower())
        {
            this.Attack();
        }
        else
        {
            Move();
        }

    }
    public override void TakeDamage(int damage)
    {
        if (HP-damage <= 0)
        {
            manager.gameManager.ModifyCoins(10); //Give a little extra
        }
        base.TakeDamage(damage);
        
    }

}
