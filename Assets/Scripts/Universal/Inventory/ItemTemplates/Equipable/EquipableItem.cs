using UnityEngine;

using Universal.Entities.Humanoids;
using Universal.Inventory.Equipping.Enums;


namespace Universal.Inventory.ItemTemplates.Equipable {
    
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