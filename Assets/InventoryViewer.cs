using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.IO;

public class InventoryViewer : MonoBehaviour
{

	public GameObject[] items;
	public void GetInventory(){
		PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), LogSuccess, FailureCallback);
	}

	
	void LogSuccess(GetUserInventoryResult guir){
		List<string> itemNames = new List<string>();
		for(int i = 0; i < guir.Inventory.Count;i++){
    		Debug.Log(guir.Inventory[i].DisplayName);
    		itemNames.Add(guir.Inventory[i].DisplayName);
    	}
    	showItems(itemNames);
    }

    void FailureCallback(PlayFabError pfe){
		Debug.Log("Error");
	}


	public void showItems(List<string> itemNames){
		List<GameObject> playerItems = new List<GameObject>();
		playerItems.Add(items[0]);
		for(int i = 1; i < items.Length;i++){
			if(itemNames.Contains(items[i].name)){
				Debug.Log("Object found with " + items[i].name);
				playerItems.Add(items[i]);
			}
		}
		for(int i = 0; i < playerItems.Count;i++){
			displayedItems.Add(Instantiate(playerItems[i], new Vector3((i - 1) * 5.5F, -1f, 5f), Quaternion.identity));
			displayedItems[displayedItems.Count - 1].transform.parent = menuRB.transform;
		}
		placeStoppers();
	}

	void placeStoppers(){
		Vector3 pos2 = new Vector3(displayedItems[displayedItems.Count - 1].transform.position.x + 3,displayedItems[displayedItems.Count - 1].transform.position.y + 3,displayedItems[displayedItems.Count - 1].transform.position.z);
		Instantiate(block, new Vector3(displayedItems[0].transform.position.x - 3,displayedItems[0].transform.position.y + 3,displayedItems[0].transform.position.z),Quaternion.identity);
		Instantiate(block,pos2,Quaternion.identity);
		menuRB.transform.position = new Vector3(pos2.x - 1, pos2.y,pos2.z);

	}


	void addForce(float force){
		//menuRB.AddForce(new Vector3(force,0,0));
		menuRB.velocity = menuRB.velocity + new Vector3(force,0,0);
	}

	public Rigidbody menuRB;

	List<GameObject> displayedItems = new List<GameObject>();


	GameObject getMiddleBat(){
		GameObject batInTheMiddle = null;
		float smallestDistance = 900f;
		for(int i = 0; i < displayedItems.Count;i++){
			if(Vector3.Distance(displayedItems[i].transform.position,Vector3.zero) < smallestDistance){
				batInTheMiddle = displayedItems[i];
				smallestDistance = Vector3.Distance(batInTheMiddle.transform.position,Vector3.zero);
			}
		}

		return batInTheMiddle;
	}

	public float xSpeed;
	bool goingRight;
	public GameObject block;
	void Update(){


		if(Input.GetKey(KeyCode.RightArrow)){
		//	moveAllItems(xSpeed);
		//	goingRight = true;
			addForce(xSpeed);
		}
		if(Input.GetKeyUp(KeyCode.RightArrow)){
			//StartCoroutine("decelerateInventory");
		}

		if(Input.GetKey(KeyCode.LeftArrow)){
		//	moveAllItems(-xSpeed);
		//	goingRight = false;
			addForce(-xSpeed);

		}
		if(Input.GetKeyUp(KeyCode.LeftArrow)){
		//	StartCoroutine("decelerateInventory");
		}



	}

	public float decelAmount;

	IEnumerator decelerateInventory(){
		Debug.Log("Decelerating");
		float newSpeed;
		float decelerationFloat;
		if(goingRight){
			newSpeed = xSpeed;
			decelerationFloat = decelAmount;
		}else{
			newSpeed = -xSpeed;
			decelerationFloat = -decelAmount;
		}

		while(Mathf.Abs(newSpeed) > 0.01f){
			moveAllItems(newSpeed);
			newSpeed = newSpeed - decelerationFloat;
			yield return new WaitForSeconds(0.01f);
		}

	}

	void moveAllItems(float amount){
		for(int i = 0; i < displayedItems.Count; i++){
			displayedItems[i].transform.position = new Vector3(displayedItems[i].transform.position.x + amount,displayedItems[i].transform.position.y,displayedItems[i].transform.position.z);
		}
	}

}
