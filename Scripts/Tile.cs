using Godot;
using SqueezecatStage.Scripts;
using System;
using System.Data;

public partial class Tile : Node2D
{
	public int row;
	public int column;
	public Tower tower = null;
	public LaneManager manager;

	public Tile()	{}

	public override void _Ready()
	{
		base._Ready();
		Area2D interactArea = GetNode<Area2D>("Area2D");
		interactArea.Connect("input_event", new Callable(this, nameof(OnInputEvent)));
	}

	public void setRowColumn(int row, int column)
	{
		this.row = row;
		this.column = column;
	}

	public bool isThereATowerOnMe()
	{
		return tower != null;
	}

	public void placeTower(Tower tower)
	{
		this.tower = tower;
	}

	public void removeTower()
	{
		this.tower = null;
	}

	private void OnInputEvent(Node viewport, InputEvent inputEvent, int shapeIdx)
	{
		
		if(inputEvent is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)//Surely there is a better way to test for left click
		{
			if(manager.gameManager.selectedTower == null || isThereATowerOnMe())
			{
				return;
			}
            manager.gameManager.towerManager.PlaceTower(this, manager.gameManager.selectedTower.codeName);
            manager.gameManager.ModifyCoins(-manager.gameManager.selectedTower.cost);
        }
	}
}
