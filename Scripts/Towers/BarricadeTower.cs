using Godot;
using System;

public partial class BarricadeTower : Tower
{
    public override void _Ready()
    {
        base.HP = 1;
        base.fireCooldown = int.MaxValue;
        base._Ready();
    }
}
