using Godot;
using System;

public partial class LaneManager : Node2D
{
	PackedScene tileScene;
	public GameManager gameManager;
	public int laneLength = 10;

	public override void _Ready()
	{
		tileScene = (PackedScene)ResourceLoader.Load("res://ObjectScenes/Tile.tscn");
	}

	public void SpawnTiles()
	{
		for (int lane = 0; lane < 3; lane++)
		{
			for (int row = 0; row < laneLength; row++)
			{
				Tile tileInstance = tileScene.Instantiate<Tile>();
				tileInstance.Position = new Vector2((row * 64)+32+(lane*16), (lane*64)+32) + gameManager.lanePosition;
				tileInstance.setRowColumn(row, lane);
				tileInstance.manager = this;
				tileInstance.ZIndex = lane * 2;
				AddChild(tileInstance);
				if(row == 0)
				{
                    gameManager.towerManager.PlaceTower(tileInstance, "BarricadeTower");
				}
			}
		}
	}
}
