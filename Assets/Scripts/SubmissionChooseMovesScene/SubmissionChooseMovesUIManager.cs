using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using Universal.Entities.Humanoids;


namespace SubmissionChooseMovesScene {
    
    public class SubmissionChooseMovesUIManager : MonoBehaviour {

        public Player player;
        
        public GameObject queueScrollContent;
        public GameObject optionScrollContent;
        
        public GameObject moveQueuePrefab;
        public GameObject moveOptionPrefab;

        public readonly List<MoveQueue> moveQueue = new List<MoveQueue>();
        public readonly List<MoveOption> moveOptions = new List<MoveOption>();

        private void Start() {
            
            this.BuildMoveOptions();
            this.BuildMoveQueue();
        }

        public void BuildMoveQueue() {

            //@TODO
            string[] movesFromApi = new string[] {};

            foreach (string move in movesFromApi) {
             
                this.AddMoveToQueue(move);
            }
        }

        public void BuildMoveOptions() {
            
            //@TODO
            string[] selectableMovesFromApi = new string[] {

                "Hip Hop Dancing 1",
                "Chicken Dance",
                "Ymca Dance",
                "Flair",
                "Gangnam Style",
                "Dancing Twerk",
                "Samba Dancing",
                "Macarena Dance",
                "Hip Hop Dancing 2",
                "Joyful Jump",
                "The Wave"
            };

            foreach (string move in selectableMovesFromApi) {

                this.AddMoveToOptions(move);
            }
        }

        public void AddMoveToOptions(string move) {
            
            GameObject createdOptionObject = Instantiate(this.moveOptionPrefab, this.optionScrollContent.transform);
            MoveOption optionMove = createdOptionObject.GetComponent<MoveOption>();

            optionMove.Build(move, this);

            this.moveOptions.Add(optionMove);
        }
        
        public void RemoveMoveFromOptions(MoveOption move) {
            
            Destroy(move.gameObject);
            this.moveOptions.Remove(move);
        }
        
        public void AddMoveToQueue(string move) {

            GameObject createdQueueObject = Instantiate(this.moveQueuePrefab, this.queueScrollContent.transform);
            MoveQueue queueMove = createdQueueObject.GetComponent<MoveQueue>();

            queueMove.Build(move, this);

            this.moveQueue.Add(queueMove);
        }
        
        public void RemoveMoveFromQueue(MoveQueue move) {
            
            Destroy(move.gameObject);
            this.moveQueue.Remove(move);
        }

        public void Play(MoveQueue move) {

            this.player.StopAnimations();
            
            this.player.animationQueue.Clear();
            
            for (int i = this.moveQueue.IndexOf(move); i < this.moveQueue.Count; i++) {

                this.player.animationQueue.Add(this.moveQueue[i].move);
            }

            //move.SetPlaying(true);
            this.player.PlayAnimations();
        }

        public void OnAnimationCompleted() {
            
            //@TODO SetPlaying();
        }

        public void SubmitMoves() {

            //@TODO: POST
            
            SceneManager.LoadScene("ConfirmSubmissionScene");
        }

        public void CancelMoves() {

            SceneManager.LoadScene("SubmissionCreateCharacter");
        }
    }
}