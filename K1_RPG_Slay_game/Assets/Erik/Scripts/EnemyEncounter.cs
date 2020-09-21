﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DISCUSS: If the OnEnemyCreate void will become an abstract void, will this still be needed?
public enum EnemyType
{
    LIGHT_ENEMY,
    HEAVY_ENEMY
}

public class EnemyEncounter : IMapEncounter, ISpawnable
{
    public int Difficulty { get; set; }
    public Vector2 ObjectPos { get; set; }

    protected GameObject _enemyMapObject;

    protected SpriteRenderer _enemySR;
    protected Sprite _selectedImage;
    protected Sprite _deselectedImage;

    public virtual Vector2 GetPosition()
    {
        return ObjectPos;
    }

    //TODO: I think the Difficulty property isn't implemented correctly. Fix it?
    public virtual int GetDifficulty()
    {
        return Difficulty;
    }

    public virtual void SetDifficulty(int _diff)
    {
        Difficulty = _diff;
    }

    public virtual void OnSelect()
    {
        _enemySR.sprite = _selectedImage;
    }

    public virtual void OnDeselect()
    {
        _enemySR.sprite = _deselectedImage;
    }

    public virtual void PickSelection()
    {
        //TODO: Confirmation Selection of encounter
    }
    
    //DISCUSS: Does this function need to be moved to the more specific inherit class? Does it need an abstract void?
    public virtual void OnEnemyCreate(EnemyType enemyType)
    {
        if(enemyType == EnemyType.LIGHT_ENEMY)
        {
            _enemyMapObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LightEnemyPrefab"));
            _enemySR = _enemyMapObject.GetComponent<SpriteRenderer>();
            _selectedImage = Resources.Load<Sprite>("Sprites/lightEnemy");
            _deselectedImage = Resources.Load<Sprite>("Sprites/lightEnemyDeselected");

            DisableGameobject();
        }
        else if (enemyType == EnemyType.HEAVY_ENEMY)
        {
            _enemyMapObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/HeavyEnemyPrefab"));
            _enemySR = _enemyMapObject.GetComponent<SpriteRenderer>();
            _selectedImage = Resources.Load<Sprite>("Sprites/heavyEnemy");
            _deselectedImage = Resources.Load<Sprite>("Sprites/heavyEnemyDeselected");

            DisableGameobject();
        }
    }

    public virtual void EnableGameobject(Vector2 position)
    {
        _enemyMapObject.SetActive(true);
        _enemyMapObject.transform.position = position;
        ObjectPos = position;
    }

    public virtual void DisableGameobject()
    {
        _enemyMapObject.SetActive(false);
    }
}