using UnityEngine;
using System.Collections;

public class QWEASDmove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
		float y = 0.0f;
		if (Input.GetKeyDown ("q")) {
						y += Time.deltaTime * 5.0f;
				}
		if (Input.GetKeyDown ("e")) {
			y -= Time.deltaTime * 5.0f;
				}
		transform.Translate(x, y, z);
		
	}
}
