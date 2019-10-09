using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RPG.SceneManagement {
	public class Portal : MonoBehaviour {
		// GameObject player;

		// void Start() {
		// 	player = GameObject.FindWithTag("Player");
		// }

		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player") {
				StartCoroutine(Transition());
			}
		}

		private IEnumerator Transition() {
			int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
			int nextSceneIndex = activeSceneIndex + 1;
			if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
				DontDestroyOnLoad(this.gameObject);
				yield return SceneManager.LoadSceneAsync(nextSceneIndex);
				print("Scene Loaded");
			} else {
				DontDestroyOnLoad(gameObject);
				yield return SceneManager.LoadSceneAsync(0);
				print("Scene Loaded");
			}
			
			Destroy(gameObject);
		}
	}
}