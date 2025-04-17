using Godot;
using System;

public partial class DefaultEnemy : Enemy
{
    public override void _Ready()
    {
        base._Ready();
        base.HP = 181;
        base.attackStrength = 100;
        base.budgetCost = 1;
    }
}
