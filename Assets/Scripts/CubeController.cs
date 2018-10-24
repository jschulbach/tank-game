using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeController : MonoBehaviour {
		public float speed = 7;
		public float jumpSpeed = 10;
		public float fireRate = 0.2f;
		public GameObject projectile;
		public GameObject Turret;
		public GameObject ScoreUI;

		int score = 0;
		float enableFire;
		private float distToGround;
		Rigidbody playerRigidbody;
		Rigidbody turretRigidbody;
		Vector3 movement;
		float camRayLength = 100f;
		LayerMask floorMask;

		// Use this for initialization
		private void Awake()
		{
				floorMask = LayerMask.GetMask("Floor");
				playerRigidbody = GetComponent<Rigidbody>();
				enableFire = Time.time;
				turretRigidbody = Turret.GetComponent<Rigidbody>();
		}
		private void FixedUpdate()
		{
				float h = Input.GetAxisRaw("Horizontal");
				float v = Input.GetAxisRaw("Vertical");
				if (Input.GetKeyDown(KeyCode.Space))
				{
						Jump();
				}
				else if (Input.GetKeyDown(KeyCode.E))
				{
						Instantiate(projectile, new Vector3(playerRigidbody.position.x, 0.5f, playerRigidbody.position.z), Quaternion.identity);
						Debug.Log("making one!");
				} else if (Input.GetMouseButton(0) && (Time.time >= enableFire + fireRate))
				{
						GameObject test = Instantiate(projectile, Turret.transform);
						Rigidbody projectileRigidbody = test.GetComponent<Rigidbody>();
						Vector3 forward = Turret.transform.TransformDirection(Vector3.forward);
						projectileRigidbody.AddForce(forward * 40f, ForceMode.Impulse);
						Destroy(test, 3);
						enableFire = Time.time;
				}

				Move(h, v);
				Turning(h, v);
		}

		void Move(float h, float v)
		{
				movement.Set(h, 0f, v);
				movement = movement.normalized * speed * Time.deltaTime;
				playerRigidbody.MovePosition(transform.position + movement);
				turretRigidbody.MovePosition(transform.position + movement);
		}

		void Turning(float h, float v)
		{
				Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit floorHit;
				//Gizmos.color = Color.red;
				Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward) * camRayLength;
				Debug.DrawRay(Camera.main.transform.position, direction);
				if (Physics.Raycast(camRay, out floorHit, camRayLength))
				{
						Vector3 playerToMouse = floorHit.point - Turret.transform.position;
						playerToMouse.y = 0f;

						Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
						float step1 = 300f * Time.deltaTime;
						//turretRigidbody.MoveRotation(newRotation);
						turretRigidbody.rotation = Quaternion.RotateTowards(turretRigidbody.rotation, newRotation, step1);
				}
				Vector3 bodyRotation = new Vector3(h, 0f, v);
				Quaternion newRotation2 = Quaternion.LookRotation(bodyRotation);
				/*playerRigidbody.MoveRotation(newRotation2);*/

				float step = 300f * Time.deltaTime;
				playerRigidbody.rotation = Quaternion.RotateTowards(playerRigidbody.rotation, newRotation2, step);
		}
		
		void OnTriggerEnter(Collider other)
		{
				Debug.Log("Trigger!");
				if (other.gameObject.CompareTag("Food"))
				{
						fireRate -= 0.01f;
						Destroy(other.gameObject);
						score++;
						//TextMeshPro test = ScoreUI.GetComponent<TextMeshPro>();
						//test.SetText(score.ToString());
				}
				Destroy(other.gameObject);
		}
		void Jump ()
		{
				Debug.Log(IsGrounded());
				if(IsGrounded()) { 
						Vector3 up = transform.TransformDirection(Vector3.up);
						playerRigidbody.velocity += up * jumpSpeed;
				}
		}

		bool IsGrounded() {
			 return Physics.Raycast(transform.position, -Vector3.up, 1);
		 }
}
