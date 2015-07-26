using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ManageAccountsMenu : MonoBehaviour {
	
	public Transform contentPanel;
	public GameObject accountButtonGameObjectStock;//prefab

	public Button createButton;
	public Button loadButton;
	public Button deleteButton;
	public Button mainMenuButton;

	string selectedKey;
	GameObject selectedButtonAccount;
	Button selectedAccountButton;

	// Use this for initialization
	void Start () {
		PersistenceManager.persistenceManager.LoadKeys ();

		Account temp = PersistenceManager.persistenceManager.account;
		foreach (string key in PersistenceManager.persistenceManager.keys) {
			GameObject accountButtonGameobject = Instantiate (accountButtonGameObjectStock) as GameObject;
			accountButtonGameobject.transform.SetParent (contentPanel);

			PersistenceManager.persistenceManager.LoadAccount (key);

			AccountButton accountButton = accountButtonGameobject.GetComponent <AccountButton> ();
			accountButton.accountButton.colors = MenuColors.whiteColor;
			accountButton.textUsername.text = PersistenceManager.persistenceManager.account.username;
			string capturedKey = key;//directly passing key would pass key of the very last iteration
			accountButton.accountButton.onClick.AddListener (() => Select (capturedKey, accountButtonGameobject, accountButton.accountButton));
		}
		PersistenceManager.persistenceManager.account = temp;

		createButton.colors = MenuColors.yellowColor;
		loadButton.colors = MenuColors.magentaColor;
		deleteButton.colors = MenuColors.whiteColor;
		mainMenuButton.colors = MenuColors.redColor;
		
		loadButton.interactable = false;
		deleteButton.interactable = false;

		createButton.onClick.AddListener (() => Create ());
		loadButton.onClick.AddListener (() => Load ());
		deleteButton.onClick.AddListener (() => Delete ());
		mainMenuButton.onClick.AddListener (() => MainMenu ());

		AccountCanvas.accountCanvas.gameObject.SetActive (true);//probably unnecessary now
		AccountCanvas.accountCanvas.GetComponent<Canvas> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Select (string key, GameObject buttonAccount, Button accountButton) {
		if (selectedAccountButton != null) {
			selectedAccountButton.colors = MenuColors.whiteColor;
		}
		accountButton.colors = MenuColors.cyanColor;

		selectedKey = key;
		selectedButtonAccount = buttonAccount;
		selectedAccountButton = accountButton;
		
		loadButton.interactable = true;
		deleteButton.interactable = true;
	}

	void Create () {
		Application.LoadLevel ("Create Account Menu");
	}
	
	void Load () {
		PersistenceManager.persistenceManager.LoadAccount (selectedKey);

		AccountCanvas.accountCanvas.UpdateAccountPanel ();
		PersistenceManager.persistenceManager.hasAccount = true;
	}
	
	void Delete () {
		PersistenceManager.persistenceManager.DeleteAccount (selectedKey);
		PersistenceManager.persistenceManager.keys.Remove (selectedKey);
		PersistenceManager.persistenceManager.SaveKeys ();

		Destroy (selectedButtonAccount);
		loadButton.interactable = false;
		deleteButton.interactable = false;

		AccountCanvas.accountCanvas.ResetAccountPanel ();
		PersistenceManager.persistenceManager.hasAccount = false;
	}
	
	void MainMenu () {
		Application.LoadLevel ("Main Menu");
	}
}