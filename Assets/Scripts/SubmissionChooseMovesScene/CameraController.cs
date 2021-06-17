using System;

using UnityEngine;


namespace SubmissionChooseMovesScene {
    
    public class CameraController : MonoBehaviour {

        public GameObject playerGmo;

        private void Update() {

            Vector3 playerPos = this.playerGmo.transform.position;

            Transform cameraTransform = this.transform;
            cameraTransform.position = new Vector3(playerPos.x, cameraTransform.position.y, (playerPos.z + 3.5f)); //3.191f
        }
    }
}