﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerClass
{
	INVALID,
	VITOP,
	STRONK,
	DEXEUS
}

public class GameManager : MonoBehaviour
{
	//Singleton pattern
	private static GameManager INSTANCE;
	public static GameManager Instance
	{
		get { return INSTANCE; }
	}

	//Definitions for other Managers
	public EncounterManager _em;
	public InputManager _im;
	public ScenesManager _sm;

	//defining a SelectButton for the input
	public SelectButton _selectButton;
	public CombatHandler _combatHandler;
	public CombatDisplay _combatDisplay;

	//Weapon Constructor to instantiate all weapons: weight, baseDamage, strScaling, dexScaling
	//DISCUSS: Another option is making a base class Weapon and adding an enum/type of for example rapier
	static IWeapon RAPIER = new Rapier(6, 10, 2, 0, 1);
	static IWeapon BOW = new Bow(6, 5, 1.5f, 0, 5);
	static IWeapon SHORTSWORD = new Shortsword(4, 5, 1f, 0.5f, 1);
	static IWeapon SWORD = new Sword(6, 5, .5f, .5f, 1);
	static IWeapon GREATSWORD = new Greatsword(10, 10, .5f, 1f, 2);
	static IWeapon BATTLEAXE = new Battleaxe(10, 10, 0f, 1.5f, 1);
	static IWeapon GREATHAMMER = new Greathammer(30, 30, 0f, 2f, 2);

	//Armor Constructor to instantiate all armors: weight, resistance
	static IArmor MAGIC_ARMOR = new MagicArmor(10, 12);
	static IArmor RAGS_ARMOR = new RagsArmor(8, 6);
	static IArmor LIGHT_ARMOR = new LightArmor(20, 8);
	static IArmor LEATHER_ARMOR = new LeatherArmor(25, 10);
	static IArmor SOLDIER_ARMOR = new SoldierArmor(35, 15);
	static IArmor HEAVY_ARMOR = new HeavyArmor(45, 20);
	static IArmor TANK_ARMOR = new TankArmor(60, 30);

	//Base formula numbers used in calculations for secondary stats
	public static int BASEHEALTH = 10;
	public static int HEALTHVITMODIFIER = 2;
	public static int BASEWEIGHTLIMIT = 20;
	public static int WEIGHTLIMITSTRMODIFIER = 2;
	public static int BASEMOVEPOINTS = 5;
	public static int MOVEPOINTSDEXMODIFIER = 5;
	public static int MOVEPOINTSWEIGHTMODIFIER = 10;

	public ICombatant _player;
	public ICombatant _currentEnemy;

	private Scene _currentScene;

	private void Awake()
	{
		Debug.Log("Initiating GameManager");

		//Singleton pattern
		if (INSTANCE != null && INSTANCE != this)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(this);
			INSTANCE = this;
		}

		//(example) Enemy constructor: vit, str, dex, weight
		//ICombatant enemy1 = new Enemy(2, 2, 2, 30);

		//Instantiate objects
		_im = new InputManager();
		_sm = new ScenesManager();

		//Check the current scene
		_currentScene = SceneManager.GetActiveScene();

		//This happens here because it's the best way to detect if it's the start of the game, and this needs to happen then
		if (_currentScene.buildIndex == 0)
		{
			_selectButton = new SelectButton(Resources.Load<Sprite>("Sprites/PlayerSelect"),
				Resources.Load<Sprite>("Sprites/PlayerDeselect"),
				Resources.Load<GameObject>("Prefabs/Button"),false);

			_im.OnLeftButtonPressed += _selectButton.SelectedActionLeft;
			_im.OnRightButtonPressed += _selectButton.SelectedActionRight;
			_im.OnSelectButtonPressed += _selectButton.Use;
		}
		if (_currentScene.buildIndex == 1)
		{
			//instantiate a new encountermanager script which handles the map logic
			_em = new EncounterManager();

			_im.OnLeftButtonPressed += _em.SelectedEncounterLeft;
			_im.OnRightButtonPressed += _em.SelectedEncounterRight;
			_im.OnSelectButtonPressed += _em.Use;
		}
		else if (_currentScene.buildIndex == 2)
		{
			// just for playability
			_player = new Player(5, 1, 1, PlayerClass.VITOP, SWORD, LEATHER_ARMOR); 
			_currentEnemy = new Enemy(2,2,2,2);

			_selectButton = new SelectButton(Resources.Load<Sprite>("Sprites/PlayerSelect"),
			Resources.Load<Sprite>("Sprites/PlayerDeselect"),
			Resources.Load<GameObject>("Prefabs/Button"), true);
			
			
			_combatDisplay = new CombatDisplay(Resources.Load<GameObject>("Prefabs/dis"),
			Resources.Load<GameObject>("Prefabs/moveP"),
			Resources.Load<GameObject>("Prefabs/Php"),
			Resources.Load<GameObject>("Prefabs/EEhp"), 
			Resources.Load<GameObject>("Prefabs/Canvas"));
			_combatHandler = new CombatHandler(1, false, 1, 9, _player as IPlayer, true, _currentEnemy as IEnemy,_combatDisplay);
			_im.OnLeftButtonPressed += _selectButton.SelectedActionLeft;
			_im.OnRightButtonPressed += _selectButton.SelectedActionRight;
			_im.OnSelectButtonPressed += _selectButton.Use;
		}
		if (_currentScene.buildIndex == 3)
		{
			
		}
	}

    private void Update()
    {
		_im.UpdateInputs();
	}
    
	//This function should be called to switch a Scene
	public void SceneSwitch()
	{
		_sm.SceneSwitch();
	}

	//This method instantiates a player with the right stats for his class at the start of the game
	public void ChooseClass(PlayerClass playerClass)
	{
		switch (playerClass)
		{
			case PlayerClass.VITOP:
				_player = new Player(5, 1, 1, playerClass, SWORD, LEATHER_ARMOR);
				break;
			case PlayerClass.STRONK:
				_player = new Player(1, 5, 1, playerClass, GREATSWORD, SOLDIER_ARMOR);
				break;
			case PlayerClass.DEXEUS:
				_player = new Player(1, 1, 5, playerClass, SHORTSWORD, LIGHT_ARMOR);
				break;
			default:
				Debug.Log("Invalid player class was chosen");
				break;
		}
	}
	
}
