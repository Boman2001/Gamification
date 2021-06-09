using Entities.Humanoids;

using UnityEngine;


namespace Inventory.ItemTemplates {
    
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject {

        public int id = 0;
        new public string name = "New Item";
        public string description = "New Item Description";
        public Sprite icon = null;
        
        public AudioClip[] onUsedAudioClips;
        
        public virtual void Use(Player player) {}
    }
}