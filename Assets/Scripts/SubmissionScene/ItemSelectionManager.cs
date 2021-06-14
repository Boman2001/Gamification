using System;
using System.Collections.Generic;
using System.Linq;

using Entities.Humanoids;

using Inventory.Equipping.Enums;
using Inventory.ItemTemplates;
using Inventory.ItemTemplates.Equipable;

using UnityEngine;


namespace SubmissionScene {
    
    public class ItemSelectionManager : MonoBehaviour {

        public Player player;
        
        public List<Tab> tabs;
        public Tab currentTab;
        
        public GameObject cameraObject;
        public Dictionary<EquipmentLocation, Vector3> equipmentLocationCameraLocation = new Dictionary<EquipmentLocation, Vector3>() {
            
            { EquipmentLocation.HEAD, new Vector3(0, 1.65f, 4.5f) },
            { EquipmentLocation.CHEST, new Vector3(0, 1.2f, 5.65f) },
        };

        public GameObject itemDisplayPrefab;

        private void Start() {

            if (this.tabs.Count > 0) {
                
                this.SetTab(0);
            }
        }

        public void SetTab(int index) {

            Tab tab = this.tabs[index];
            this.cameraObject.transform.localPosition = this.equipmentLocationCameraLocation[tab.equipmentLocation];

            if (this.currentTab != null) {
                
                this.WipeItemPanel(this.currentTab.itemPanel);
            }

            this.BuildItemPanel(tab.itemPanel, tab.equipmentLocation);
            
            this.currentTab = tab;
            tab.panel.SetAsFirstSibling();
        }

        public void BuildItemPanel(GameObject itemPanel, EquipmentLocation equipmentLocation) {

            if (itemPanel == null) {

                return;
            }
            
            IEnumerable<Item> items = this.player.inventory.items.Where
            (
                (Item item) => item is EquipableItem equipableItem
                    && equipableItem.equipmentLocation == equipmentLocation
            );

            foreach (Item equipableItem in items) {

                ItemDisplay itemDisplay = Instantiate(
                    
                    this.itemDisplayPrefab,
                    itemPanel.transform
                )
                .GetComponent<ItemDisplay>();

                itemDisplay.icon.sprite = equipableItem.icon;
                itemDisplay.itemButton.onClick.AddListener(
                    
                    () => {

                        this.player.inventory.equipmentManager.Equip((EquipableItem)equipableItem);
                    }
                );
            }
        }
        
        public void WipeItemPanel(GameObject itemPanel) {

            if (itemPanel == null) {

                return;
            }
            
            foreach (Transform child in itemPanel.transform) {
             
                Destroy(child.gameObject);
            }
        }
    }
}