using UnityEngine;
using System.Collections;

public class SpaceCapsuleCollisionDetecter : MonoBehaviour {

	/*
	 * SpaceCapsule prefab has a BoxCollider and a rigidbody (Rigidbody Collider)
	 * Player (MainCamera) has a CapsuleCollider and no rigidbody (Static Collider)
	 * 
	 */

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool CanInteractWith = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("SpaceCapsuleCollisionDetecter::OnTriggerEnter() - Collider: " + other + " ; GameObject=" + other.gameObject.name);
        if (other.gameObject.name == "Terrain")
            return;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("SpaceCapsuleCollisionDetecter::OnTriggerExit() - Collider: " + other + " ; GameObject=" + other.gameObject.name);
        if (other.gameObject.name == "Terrain")
            return;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("SpaceCapsuleCollisionDetecter::OnTriggerStay() - Collider: " + other + " ; GameObject=" + other.gameObject.name);
        if (other.gameObject.name == "Terrain")
            return;
    }


    void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "MainCamera") {
			Debug.Log ("SpaceCapsuleCollisionDetecter::OnCollisionEnter() - Collision: " + collision + " ; GameObject="+collision.gameObject.tag);
			CanInteractWith = true;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "MainCamera") {
			Debug.Log ("SpaceCapsuleCollisionDetecter::OnCollisionExit() - Collision: " + collision + " ; GameObject="+collision.gameObject.tag);
			this.gameObject.GetComponent<Renderer>().material.color = Color.white;
			CanInteractWith = false;
		}
	}


	void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag == "MainCamera") {
			Debug.Log ("SpaceCapsuleCollisionDetecter::OnCollisionStay() - Collision: " + collision + " ; GameObject=" + collision.gameObject.tag);
			this.gameObject.GetComponent<Renderer>().material.color = Color.red;
			CanInteractWith = true;
		}
	}

	void OnGUI(){
		if (CanInteractWith) {
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 150, 30), "Can interact with");
		}
		else {
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 150, 30), "Can't interact with");
		}
	}
}
