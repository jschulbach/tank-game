using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGame : MonoBehaviour {

	public GameObject food;
		//private TextMeshPro m_textMeshPro;
		//private TMP_FontAsset m_FontAsset;

		private const string label = "The <#0050FF>count is: </color>{0:2}";
		private float m_frame;
		// Use this for initialization
		void Start () {
		for (int x = 0; x < 150; x++)
		{
						float foodX = Random.value * 80 - 40;
						float foodY = Random.value * 80 - 40;
				Instantiate(food, new Vector3(foodX, 0.5f, foodY), Quaternion.identity);
		}

				//m_textMeshPro = gameObject.AddComponent<TextMeshPro>();

				//m_textMeshPro.autoSizeTextContainer = true;
				//m_textMeshPro.fontSize = 48;

				//m_textMeshPro.alignment = TextAlignmentOptions.Center;
				//m_textMeshPro.enableWordWrapping = false;
		}
	
	// Update is called once per frame
	void Update () {
				if(Random.value > 0.98)
				{
						float foodX = Random.value * 80 - 40;
						float foodY = Random.value * 80 - 40;
						Instantiate(food, new Vector3(foodX, 0.5f, foodY), Quaternion.identity);
				}
				//m_textMeshPro.SetText(label, m_frame % 1000);
				//m_frame += 1 * Time.deltaTime;
		}
}