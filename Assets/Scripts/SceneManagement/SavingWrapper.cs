using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement {
	public class SavingWrapper : MonoBehaviour {
		const string defaultSaveFile = "save";

		SavingSystem saveSys;

		void Start() {
			saveSys = GetComponent<SavingSystem>();
		}

		void Update() {
			if (Input.GetKeyDown(KeyCode.L)) {
				Load();
			}
			
			if (Input.GetKeyDown(KeyCode.S)) {
				Save();
			}
		}

		void Load() {
			saveSys.Load(defaultSaveFile);
		}

		void Save() {
			saveSys.Save(defaultSaveFile);
		}
	}
}