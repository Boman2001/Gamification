using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


namespace Universal.Entities.Humanoids {
    
    public class Player : MonoBehaviour {

        public int coins;
        public Inventory.Inventory inventory;

        [Header("Animation")]
        public Animator animator;
        public Dictionary<string, int> animationHashes = new Dictionary<string, int>();
        
        public List<string> animationQueue;
        public int repeatAnimationFor = 1;
        public float fadeDuration = 0.6f;
        
        public UnityEvent onAnimationCompleted;

        public void BuildAnimationHashes() {

            this.animationHashes.Clear();

            foreach (string animationName in this.animationQueue) {

                this.animationHashes[animationName] = Animator.StringToHash("Base Layer." + animationName);
            }
        }
        
        public void PlayAnimations() {

            this.BuildAnimationHashes();
            this.StartCoroutine(nameof(this.WorkAnimationQueue));
        }

        public void StopAnimations() {
            
            this.StopCoroutine(nameof(this.WorkAnimationQueue));
        }

        private IEnumerator WorkAnimationQueue() {
            
            foreach (string anim in this.animationQueue) {
                
                //Fade into the next animation.
                this.animator.CrossFadeInFixedTime(anim, this.fadeDuration);

                //Wait until animator entered the state
                while (this.animator.GetCurrentAnimatorStateInfo(0).fullPathHash != this.animationHashes[anim]) {
                    
                    yield return null;
                }

                //Wait until complete.
                AnimatorStateInfo animStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
                yield return new WaitForSeconds(
                    
                    (
                        animStateInfo.length - this.fadeDuration
                        //* animStateInfo.normalizedTime
                    )
                    * this.repeatAnimationFor
                );
                
                //Publish animation completed event.
                this.onAnimationCompleted.Invoke();
            }
            
            //Return to idle state.
            this.animator.CrossFadeInFixedTime("Idle", this.fadeDuration);
        }
    }
}