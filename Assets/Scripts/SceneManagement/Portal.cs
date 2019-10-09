using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement {
	public class Portal : MonoBehaviour {
		// GameObject player;

		// void Start() {
		// 	player = GameObject.FindWithTag("Player");
		// }

		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player") {
				int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
				int nextSceneIndex = activeSceneIndex + 1;
				if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
					SceneManager.LoadScene(nextSceneIndex);
				} else {
					SceneManager.LoadScene(0);
				}
			}
		}
	}
}