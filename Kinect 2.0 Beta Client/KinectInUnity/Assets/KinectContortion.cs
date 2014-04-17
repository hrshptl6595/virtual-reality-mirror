using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Collections.Specialized;

public class KinectContortion : MonoBehaviour {
	public static Dictionary<string, Vector3>[] KINECT_DATA = new Dictionary<string, Vector3>[7];
	public static GameObject[] PLAYERS = new GameObject[7];
	public GameObject PLAYER_OBJECT;
	// Use this for initialization
	void Start () {
		for (int i=1; i<=6; i++) {
			KINECT_DATA[i] = new Dictionary<string, Vector3> ();
			GameObject player = (GameObject)GameObject.Instantiate(PLAYER_OBJECT);
			PLAYERS[i] = player;
		}
	}
	
	// Update is called once per frame
	void Update () {
		WebRequest wrURL = WebRequest.Create ("http://localhost:3000/status");
		string grabbedPage = (new StreamReader (wrURL.GetResponse ().GetResponseStream ())).ReadToEnd ().Trim ();
		int body = 0;
		foreach (string line in grabbedPage.Split(new char[] {'\r', '\n'})) {
			if (line.Contains("Body:")) {
				body = int.Parse(line.Split(':')[1]);
			} else if (line.Contains(":")) {
				string[] parts = line.Split(':');
				Vector3 numbers = new Vector3();
				numbers.x = float.Parse(parts[1].Split(',')[0]);
				numbers.y = float.Parse(parts[1].Split (',')[1]);
				numbers.z = float.Parse(parts[1].Split(',')[2]);
				KINECT_DATA[body][parts[0]] = numbers;
			}
		}
		for (int i=1; i<=6; i++) {
			PLAYERS[i].BroadcastMessage("updateKinectData", KINECT_DATA[i], SendMessageOptions.RequireReceiver);
				}
	}
}
