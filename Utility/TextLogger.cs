using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An onscreen text logger
/// </summary>
public class TextLogger : MonoBehaviour {
	[SerializeField] private Text textLog;

	public static TextLogger instance;

	private List<string> logs = new List<string>();
	private const int maxLines = 10;


	void Awake() {
		instance = this;
	}

	void Update() {
		textLog.text = "";
		for (int i = 0; i < logs.Count; i++) {
			textLog.text = textLog.text + logs[i] + '\n';
		}
	}

	/// <summary>
	/// Log information to a UI Text component
	/// </summary>
	/// <param name="source">Text containing where this log originates</param>
	/// <param name="text">Text to appear on screen</param>
	/// <param name="logText">Also log text to console</param>
	public static void Log (string source, string text, bool logText=true) {
		Debug.Log(source + " :: " + text);
		instance.logs.Add(source + " :: " + text);
		if (instance.logs.Count > maxLines) {
			instance.logs.RemoveAt(0);
		}
	}
}
