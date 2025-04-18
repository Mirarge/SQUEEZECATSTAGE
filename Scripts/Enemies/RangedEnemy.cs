using Godot;
using System;

public partial class RangedEnemy : Enemy
{
    public RangedEnemy()
    {
        base.HP = 100;
        base.attackStrength = 20;
        base.budgetCost = 10;
        base.SightDistance = 5;
        base.attackCooldown = base.baseCooldown = 200;
    }
    public override void Attack()
    {
        if (attackCooldown > 0) attackCooldown--;
        else
        {
            attackCooldown = baseCooldown;
            manager.gameManager.projectileManager.SpawnProjectile("RangedEnemyProjectile", Position, new Vector2(-2, 0), this.ZIndex + 1, false);
        }
    }
}
