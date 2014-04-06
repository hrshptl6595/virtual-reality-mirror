using UnityEngine;
using System.Collections;
using Leap;
using System.Net;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System;

public class HandController : MonoBehaviour {
	public GameObject[] fingers;
	public GameObject[] colliders;

	private LeapManager _leapManager;
	// Use this for initialization
	void Start () {
		_leapManager = (GameObject.Find("LeapManager")as GameObject).GetComponent(typeof(LeapManager)) as LeapManager;
	}
	
	// Update is called once per frame
	void Update () {
		Hand primeHand = _leapManager.frontmostHand();

		if(primeHand.IsValid)
		{
			Vector3[] fingerPositions = _leapManager.getWorldFingerPositions(primeHand);
			gameObject.transform.position = primeHand.PalmPosition.ToUnityTranslated();
			if(gameObject.renderer.enabled != true) { gameObject.renderer.enabled = true; }

			string handFingers = "Body:1\n"; // String that stores all five finger positions for the detected hand

			for(int i=0;i<fingers.GetLength(0);i++)
			{
				if(i < fingerPositions.GetLength(0))
				{
					fingers[i].transform.position = fingerPositions[i];
					if(fingers[i].renderer.enabled == false) { fingers[i].renderer.enabled = true; }

					if(colliders != null && i < colliders.GetLength(0))
					{
						(colliders[i].GetComponent(typeof(SphereCollider)) as SphereCollider).enabled = true;
					}

					// Write finger positions to server
					handFingers += "Finger" + i + "Right:" + fingers[i].transform.position.x + "," + fingers[i].transform.position.y + "," + fingers[i].transform.position.z + "\n";


				}
				else
				{
					fingers[i].renderer.enabled = false;
					if(colliders != null && i < colliders.GetLength(0))
					{
						(colliders[i].GetComponent(typeof(SphereCollider)) as SphereCollider).enabled = false;
					}
				}
			}

			// Write finger positions to server
			if (fingers.GetLength(0) > 0) {
				using (var wb = new WebClient()) {
					var data = new NameValueCollection();
					data["username"] = "myUser";
					data["password"] = "myPassword";
					data["data"] = handFingers;
					var response = wb.UploadValues("http://10.1.41.246:3000/leap/right", "POST", data);
					var worldState = Encoding.ASCII.GetString(response);
				}
			}
		}
		else
		{
			gameObject.renderer.enabled = false;

			foreach(GameObject finger in fingers)
			{
				if(finger.renderer.enabled == true) { finger.renderer.enabled = false; }
			}
		}

	}
}
