using Godot;
using System;

public partial class StrongerEnemy : Enemy
{
    public StrongerEnemy()
    {
        base.HP = 200;
        base.attackStrength = 150;
        base.budgetCost = 10;
        base.speed = 0.3f;
    }
}
