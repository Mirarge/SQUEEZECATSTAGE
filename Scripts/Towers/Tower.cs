using Godot;
using System;

public partial class Tower : Node2D
{
    public int HP;
    public int fireCooldown = 0;

    public void Fire()
    {
        if(fireCooldown > 0) fireCooldown--;
        else
        {
            GD.Print("I AM FIRING");
            fireCooldown = 10;
        }
    }
}
