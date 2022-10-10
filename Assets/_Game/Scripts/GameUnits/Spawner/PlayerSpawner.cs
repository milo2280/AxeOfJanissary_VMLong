using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Spawner
{
    private void Awake()
    {
        spawnedCharacters = new Character[DataManager.Instance.PlayerAmount];
    }

    protected override void Spawn()
    {
        index = (int)DataManager.Instance.CurrentPlayerCharacter;

        if (character != null) character.gameObject.SetActive(false);

        if (spawnedCharacters[index] == null)
        {
            CharacterData data = DataManager.Instance.GetPlayerData((CharacterType)index);
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
