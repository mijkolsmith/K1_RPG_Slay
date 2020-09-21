﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemyEncounter : EnemyEncounter, IMapEncounter, ISpawnable
{
    //DISCUSS: Can this be done better? Does the code from EnemyEncounter need to be moved to this class instead? And are these overrides even needed here?

    public LightEnemyEncounter()
    {
        OnEnemyCreate(EnemyType.LIGHT_ENEMY);
        base.SetDifficulty(1);
    }

    public override int GetDifficulty()
    {
        return base.GetDifficulty();
    }

    public override void OnSelect()
    {
        base.OnSelect();
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
    }

    public override void PickSelection()
    {
        GameManager.Instance._currentEnemy = new Enemy(1, 1, 5, 12);
        base.PickSelection();
    }

    public override void OnEnemyCreate(EnemyType enemyType)
    {
        base.OnEnemyCreate(enemyType);
    }

    public override void EnableGameobject(Vector2 position)
    {
        base.EnableGameobject(position);
    }

    public override void DisableGameobject()
    {
        base.DisableGameobject();
    }
}
