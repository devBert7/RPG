using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace RPG.SceneManagement {
	public class Portal : MonoBehaviour {
		[SerializeField] Transform spawnPoint;
		[SerializeField] DestinationID destination;

		enum DestinationID {
			A,
			B,
			C,
			D,
			E
		};

		void OnTriggerEnter(Collider other) {
			if (other.tag == "Player") {
				StartCoroutine(Transition());
			}
		}

		private IEnumerator Transition() {
			int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
			int nextSceneIndex = activeSceneIndex + 1;

			DontDestroyOnLoad(gameObject);

			if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
				yield return SceneManager.LoadSceneAsync(nextSceneIndex);
			} else {
				yield return SceneManager.LoadSceneAsync(0);
			}
			
			Portal otherPortal = GetOtherPortal();
			UpdatePlayerPos(otherPortal);
			Destroy(gameObject);
		}

		Portal GetOtherPortal() {
			foreach (Portal portal in FindObjectsOfType<Portal>()) {
				if (portal == this) {
					continue;
				}

				if (portal.destination != destination) {
					continue;
				}

				return portal;
			}

			return null;
		}

		void UpdatePlayerPos(Portal otherPortal) {
			GameObject player = GameObject.FindWithTag("Player");
			player.transform.position = otherPortal.spawnPoint.position;
			player.transform.rotation = otherPortal.spawnPoint.rotation;
		}
	}
}