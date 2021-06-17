using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using Universal.Entities.Humanoids;
using Universal.Inventory.Equipping.Enums;
using Universal.Inventory.ItemTemplates;
using Universal.Inventory.ItemTemplates.Equipable;
using Universal.UI;


namespace SubmissionCreateCharacterScene {
    
    public class SubmissionCreateCharacterUIManager : MonoBehaviour {

        public Player player;
        
        public List<EquipTab> tabs;
        public EquipTab currentTab;
        
        public GameObject itemDisplayPrefab;
        
        public GameObject cameraObject;
        public Dictionary<EquipmentLocation, Vector3> equipmentLocationCameraLocation = new Dictionary<EquipmentLocation, Vector3>() {

            { EquipmentLocation.FACE, new Vector3(0, 1.65f, 4.5f) },
            { EquipmentLocation.HEAD, new Vector3(0, 1.65f, 4.5f) },
            { EquipmentLocation.CHEST, new Vector3(0, 1.65f, 6.5f) },
        };

        private void Start() {

            if (this.tabs.Count > 0) {
                
                this.SetTab(0);
            }
        }

        public void SubmitCharacter() {
            
            //@TODO: POST
            
            SceneManager.LoadScene("SubmissionChooseMoves");
        }

        public void CancelCharacter() {

            SceneManager.LoadScene("Home");
        }

        public void SetTab(int index) {

            for (int i = 0; i < this.tabs.Count; i++) {

                this.tabs[i].tabInner.SetActive(i == index);
            }
            
            EquipTab tab = this.tabs[index];
            this.cameraObject.transform.localPosition = this.equipmentLocationCameraLocation[tab.equipmentLocation];

            if (this.currentTab != null) {
                
                this.WipeItemPanel(this.currentTab.itemPanel);
            }

            this.BuildItemPanel(tab.itemPanel, tab.equipmentLocation);
            
            this.currentTab = tab;
            tab.tabRoot.SetAsFirstSibling();
        }

        private void BuildItemPanel(GameObject itemPanel, EquipmentLocation equipmentLocation) {

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
        
        private void WipeItemPanel(GameObject itemPanel) {

            if (itemPanel == null) {

                return;
            }
            
            foreach (Transform child in itemPanel.transform) {
             
                Destroy(child.gameObject);
            }
        }
    }
}