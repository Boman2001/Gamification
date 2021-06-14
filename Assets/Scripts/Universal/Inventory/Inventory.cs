using System.Collections.Generic;

using UnityEngine;

using Universal.Inventory.Equipping;
using Universal.Inventory.ItemTemplates;


namespace Universal.Inventory {
    public class Inventory : MonoBehaviour {

        public EquipmentManager equipmentManager;
        public List<Item> items;
    }
}