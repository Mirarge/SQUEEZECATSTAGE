using Godot;
using System;
using System.Collections.Generic;

namespace SqueezecatStage.Scripts
{
    public partial class DataStorage : Node
    {
        public int Coins = 0;

        public Dictionary<string, PackedScene> projectileTypes = new Dictionary<string, PackedScene>();
        public Dictionary<string, PackedScene> towerTypes = new Dictionary<string, PackedScene>();
        public Dictionary<string, PackedScene> enemyTypes = new Dictionary<string, PackedScene>();
        
        public List<TowerShopDefinition> towerShopDefinitions = new List<TowerShopDefinition>();

        public static DataStorage Instance { get; private set; }
        public override void _Ready()
        {
            Instance = this;

            LoadProjectiles();
            LoadTowers();
            LoadEnemies();
            LoadTowerShopDefinitions();
        }
        private void LoadProjectiles()
        {
            projectileTypes.Add("TestProjectile", GD.Load<PackedScene>("res://ObjectScenes/Projectiles/Projectile.tscn"));
        }
        private void LoadTowers()
        {
            towerTypes.Add("TestTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/Tower.tscn"));
            towerTypes.Add("CannonTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/CannonTower.tscn"));
            towerTypes.Add("BarricadeTower", GD.Load<PackedScene>("res://ObjectScenes/Towers/BarricadeTower.tscn"));
        }
        private void LoadEnemies()
        {
            enemyTypes.Add("TestEnemy", GD.Load<PackedScene>("res://ObjectScenes/Enemies/Enemy.tscn"));
        }
        private void LoadTowerShopDefinitions()
        {
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon Ranged", "CannonTower", 100, "Ranged"));
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon RANGED", "CannonTower", 200, "Ranged"));
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon Support", "CannonTower", 120, "Support"));
            towerShopDefinitions.Add(new TowerShopDefinition("Cannon Melee", "CannonTower", 200, "Melee"));
        }
    }
}
