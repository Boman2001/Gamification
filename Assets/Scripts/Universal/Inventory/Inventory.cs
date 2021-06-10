using System.Collections.Generic;

using Inventory.Equipping;
using Inventory.ItemTemplates;

using UnityEngine;


namespace Inventory {
    public class Inventory : MonoBehaviour {

        public EquipmentManager equipmentManager;
        public List<Item> items;
    }
}