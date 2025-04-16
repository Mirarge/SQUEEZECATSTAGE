using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqueezecatStage.Scripts
{
    public partial class TowerShopDefinition : Node
    {
        public string name;
        public string codeName;
        public int cost;
        public int unlockWave;
        public string category;
        public TowerShopDefinition(string name, string codeName, int cost, string category, int unlockWave = 0)
        {
            this.name = name;
            this.codeName = codeName;
            this.cost = cost;
            this.category = category;
            this.unlockWave = unlockWave;
        }
    }
}
