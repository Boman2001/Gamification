using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


namespace Universal.Entities.Humanoids {
    
    public class Player : MonoBehaviour {

        public int coins;
        
        public List<string> animationQueue;
        public Inventory.Inventory inventory;

        public UnityEvent onAnimationCompleted;
        
        public Animator animator;
        public int repeatAnimationFor = 1;

        public void PlayAnimations() {
            
            this.StartCoroutine(nameof(this.WorkAnimationQueue));
        }

        public void StopAnimations() {
            
            this.StopCoroutine(nameof(this.WorkAnimationQueue));
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
                
                //this.onAnimationCompleted.Invoke();
            }
            
            this.animator.Play("Idle");
        }
    }
}