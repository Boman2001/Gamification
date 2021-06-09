using Entities.Humanoids;

using Inventory.Equipping;
using Inventory.Equipping.Enums;

using UnityEngine;


namespace Inventory.ItemTemplates.Equipable {
    
    [CreateAssetMenu(fileName = "New Equipable", menuName = "Inventory/Equipable")]
    public class EquipableItem : Item {
        
        public GameObject equipPrefab;
        public EquipmentLocation equipmentLocation;
        
        public override void Use(Player player) {
            
            base.Use(player);

            player.inventory.equipmentManager.Equip(this);
        }
    }
}