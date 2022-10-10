using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : Spawner
{
    private void Awake()
    {
        spawnedCharacters = new Character[DataManager.Instance.BotAmount];
    }

    protected override void Spawn()
    {
        index = (int)LevelManager.Instance.CurrentLevelData.BotType;

        if (character != null) character.gameObject.SetActive(false);

        if (spawnedCharacters[index] == null)
        {
            CharacterData data = DataManager.Instance.GetBotData((BotType)index);
            character = Instantiate(data.Prefab, parentTransform);
            spawnedCharacters[index] = character;
            character.Data = data;
        }
        else
        {
            character = spawnedCharacters[index];
            character.gameObject.SetActive(true);
        }

        base.Spawn();
    }
}
