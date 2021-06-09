using System;
using System.Collections;

using Inventory.ItemTemplates.Equipable;

using UnityEngine;


namespace Entities.Humanoids {
    
    public class Player : MonoBehaviour {

        public int coins;
        public Inventory.Inventory inventory;
        
        public Animator animator;

        public int repeatAnimationFor = 1;
        public string[] animationQueue;

        private void Start() {

            this.StartCoroutine(nameof(this.WorkAnimationQueue));
        }

        private IEnumerator WorkAnimationQueue() {
            
            foreach (string anim in this.animationQueue) {

                this.animator.Play(anim);

                AnimatorStateInfo animStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
                
                yield return new WaitForSeconds(
                    
                    (
                        animStateInfo.length 
                        + animStateInfo.normalizedTime
                    )
                    * this.repeatAnimationFor
                );
            }
            
            this.animator.Play("Idle");
        }
    }
}