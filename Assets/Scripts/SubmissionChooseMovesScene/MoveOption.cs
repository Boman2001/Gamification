using TMPro;

using UnityEngine;


namespace SubmissionChooseMovesScene {
    public class MoveOption : MonoBehaviour {
        
        public TextMeshProUGUI label;
        
        public SubmissionChooseMovesUIManager submissionChooseMovesUIManager { get; protected set; }
        public string move { get; protected set; }
        
        public void Build(string newMove, SubmissionChooseMovesUIManager newSubmissionChooseMovesUIManager) {

            this.move = newMove;
            this.submissionChooseMovesUIManager = newSubmissionChooseMovesUIManager;

            this.Refresh();
        }

        public void Refresh() {
            
            this.label.text = this.move;
        }

        public void OnAddButtonClicked() {
            
            this.submissionChooseMovesUIManager.AddMoveToQueue(this.move);
        }
    }
}