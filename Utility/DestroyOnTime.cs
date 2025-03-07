using UnityEngine;

/// <summary>
/// Destroy a gameobject after a set time
/// </summary>
public class DestroyOnTime : MonoBehaviour {
	[SerializeField]
	private float killTime = 1f;

	void Update() {
		if (killTime <= 0f) {
			Destroy(gameObject);
		} else {
			killTime -= Time.deltaTime;
		}
	}
}
