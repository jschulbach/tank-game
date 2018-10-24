using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {
		private float y0;
		private float offset;
		// Use this for initialization
		void Start () {
		y0 = transform.position.y;
				offset = Random.value * 3f;
	}
	
	// Update is called once per frame
	void Update () {
				Vector3 temp = transform.position;
				temp.y = y0 + 0.1f * Mathf.Sin(2f * Time.time + offset);
				transform.position = temp;
	}
}
