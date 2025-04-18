using Godot;
using System;

public partial class DefaultEnemy : Enemy
{
    public DefaultEnemy()
    {
        base.HP = 181;
        base.attackStrength = 100;
        base.attackCooldown = base.baseCooldown = 100;
        base.budgetCost = 2;
        base.speed = 0.4f;
    }
}
