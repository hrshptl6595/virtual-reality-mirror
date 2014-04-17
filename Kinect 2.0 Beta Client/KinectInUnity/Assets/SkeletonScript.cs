using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkeletonScript : MonoBehaviour {
	public GameObject BONE;
	public Dictionary<string, Dictionary<string, GameObject>> BONES = new Dictionary<string, Dictionary<string, GameObject>> ();

	void createBone(string joint1, string joint2) {
		if (!(BONES.ContainsKey(joint1))) {
						BONES [joint1] = new Dictionary<string, GameObject> ();
				}
		BONES [joint1] [joint2] = (GameObject)Object.Instantiate (BONE);
		}

	// Use this for initialization
	void Start () {
		createBone ("Head", "Neck");
		createBone ("Neck", "SpineShoulder");
		createBone ("SpineShoulder", "SpineMid");
		createBone ("SpineMid", "SpineBase");
		createBone ("SpineShoulder", "ShoulderRight");
		createBone ("SpineShoulder", "ShoulderLeft");
		createBone ("SpineBase", "HipRight");
		createBone ("SpineBase", "HipLeft");
		createBone ("ShoulderRight", "ElbowRight");
		createBone ("ElbowRight", "WristRight");
		createBone ("WristRight", "HandRight");
		createBone ("HandRight", "HandTipRight");
		createBone ("WristRight", "ThumbRight");
		createBone ("ShoulderLeft", "ElbowLeft");
		createBone ("ElbowLeft", "WristLeft");
		createBone ("WristLeft", "HandLeft");
		createBone ("HandLeft", "HandTipLeft");
		createBone ("WristLeft", "ThumbLeft");
		createBone ("HipRight", "KneeRight");
		createBone ("KneeRight", "AnkleRight");
		createBone ("AnkleRight", "FootRight");
		createBone ("HipLeft", "KneeLeft");
		createBone ("KneeLeft", "AnkleLeft");
		createBone ("AnkleLeft", "FootLeft");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void updateKinectData(Dictionary<string, Vector3> KINECT_DATA) {
		appear ();//remove
		foreach (string key_1 in BONES.Keys) {
			foreach (string key_2 in BONES[key_1].Keys) {
				BONES[key_1][key_2].transform.localScale = new Vector3(BONES[key_1][key_2].transform.localScale.x,BONES[key_1][key_2].transform.localScale.y,Vector3.Distance(KINECT_DATA[key_1],KINECT_DATA[key_2])/2.0f);
				BONES[key_1][key_2].transform.position = KINECT_DATA[key_1];
				BONES[key_1][key_2].transform.LookAt(KINECT_DATA[key_2]);
			}
		}
	}
	
	void disappear() {
		foreach (string key_1 in BONES.Keys) {
			foreach (string key_2 in BONES.Keys) {
				BONES[key_1][key_2].renderer.enabled = false;
			}
		}
	}

	void appear() {
		foreach (string key_1 in BONES.Keys) {
			foreach (string key_2 in BONES.Keys) {
				BONES[key_1][key_2].renderer.enabled = true;
			}
		}
	}
}
