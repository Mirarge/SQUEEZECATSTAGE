using Godot;
using System;

public partial class WallTower : Tower
{
    public override void _Ready()
    {
        base.HP = 4000;
        base._Ready();
    }
}
