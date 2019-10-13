using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine.AI;

namespace RPG.SceneManagement {
	public class Portal : MonoBehaviour {
		[SerializeField] Transform spawnPoint;
		[SerializeField] DestinationID destination;
		// [SerializeField] float sceneToLoad = -1;
		[SerializeField] float fadeOutTime = 3f;
		[SerializeField] float fadeInTime = 1f;
		[SerializeField] float fadeWaitTime = .5f;

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

			// if (sceneToLoad < 0) {
			// 	Debug.LogError("Scene to load is not set");
			// 	yield break;
			// }

			Fader fader = FindObjectOfType<Fader>();

			DontDestroyOnLoad(gameObject);

			yield return fader.FadeOut(fadeOutTime);

			SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
			wrapper.Save();

			if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
				yield return SceneManager.LoadSceneAsync(nextSceneIndex);
			} else {
				yield return SceneManager.LoadSceneAsync(0);
			}

			wrapper.Load();

			Portal otherPortal = GetOtherPortal();
			UpdatePlayerPos(otherPortal);
			
			yield return new WaitForSeconds(fadeWaitTime);
			yield return fader.FadeIn(fadeInTime);
			
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
			player.GetComponent<NavMeshAgent>().enabled = false;
			player.transform.position = otherPortal.spawnPoint.position;
			player.transform.rotation = otherPortal.spawnPoint.rotation;
			player.GetComponent<NavMeshAgent>().enabled = true;
		}
	}
}