using UnityEngine;
using UnityEngine.SceneManagement;


namespace SubmissionChooseMovesScene {
    
    public class SubmissionChooseMovesUIManager : MonoBehaviour {

        public void SubmitMoves() {

            //@TODO: POST
            
            SceneManager.LoadScene("ConfirmSubmissionScene");
        }

        public void CancelMoves() {

            SceneManager.LoadScene("SubmissionCreateCharacter");
        }
    }
}