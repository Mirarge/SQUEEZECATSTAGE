using Godot;
using System;

public partial class FastEnemy : Enemy
{
    public FastEnemy()
    {
        base.HP = 150;
        base.attackStrength = 50;
        base.budgetCost = 6;
        base.speed = 0.7f;
    }
}
