using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Universal.Inventory.Equipping.Enums;
using Universal.Inventory.ItemTemplates.Equipable;


namespace Universal.Inventory.Equipping {

    public class EquipmentManager : MonoBehaviour {

        public Inventory inventory;
        public EquipableItem[] equipment;

        //Equipment Locations
        public GameObject leftHandEquipmentLocation;
        public GameObject rightHandEquipmentLocation;
        public GameObject twoHandedEquipmentLocation;

        public GameObject headEquipmentLocation;
        public GameObject faceEquipmentLocation;
        public GameObject chestEquipmentLocation;

        public GameObject leftLegEquipmentLocation;
        public GameObject rightLegEquipmentLocation;

        public GameObject leftFootEquipmentLocation;
        public GameObject rightFootEquipmentLocation;

        //Equipment Location Map
        private Dictionary<EquipmentLocation, GameObject> equipmentSearchMap;

        //Event Functions
        private void Awake() {

            this.equipment = new EquipableItem[
                
                System.Enum.GetNames(typeof(EquipmentLocation)).Length
            ];

            //Build search map for easy runtime searching
            this.equipmentSearchMap = new Dictionary<EquipmentLocation, GameObject>() {

                {
                    EquipmentLocation.LEFT_HAND, this.leftHandEquipmentLocation
                },
                {
                    EquipmentLocation.RIGHT_HAND, this.rightHandEquipmentLocation
                },
                {
                    EquipmentLocation.TWO_HANDED, this.twoHandedEquipmentLocation
                },
                {
                    EquipmentLocation.HEAD, this.headEquipmentLocation
                },
                {
                    EquipmentLocation.FACE, this.faceEquipmentLocation
                },
                {
                    EquipmentLocation.CHEST, this.chestEquipmentLocation
                },
                {
                    EquipmentLocation.LEFT_LEG, this.leftLegEquipmentLocation
                },
                {
                    EquipmentLocation.RIGHT_LEG, this.rightLegEquipmentLocation
                },
                {
                    EquipmentLocation.LEFT_FOOT, this.leftFootEquipmentLocation
                },
                {
                    EquipmentLocation.RIGHT_FOOT, this.rightFootEquipmentLocation
                }
            };
        }

        //Logic
        public bool Equip(EquipableItem item) {

            int slotIndex = (int) item.equipmentLocation;
            EquipableItem potentialCurrentItem = this.equipment.ElementAtOrDefault(slotIndex);

            if
            (
                potentialCurrentItem != null
                && !this.DeEquip(potentialCurrentItem.equipmentLocation)
            ) 
            {
                return false;
            }

            //Spawn Prefab
            GameObject equipablePrefab = Instantiate
            (
                item.equipPrefab,
                this.equipmentSearchMap[item.equipmentLocation].transform
            );
            
            //Set equipped item
            this.equipment[slotIndex] = item;

            //Return success.
            return true;
        }

        public bool DeEquip(EquipmentLocation equipmentLocation) {

            int slotIndex = (int) equipmentLocation;

            this.equipment[slotIndex] = null;
            this.WipeEquipmentLocation(equipmentLocation);

            return true;
        }

        public void WipeEquipmentLocation(EquipmentLocation equipmentLocation) {

            foreach (Transform child in this.equipmentSearchMap[equipmentLocation].transform) {

                Destroy(child.gameObject);
            }
        }
    }
}