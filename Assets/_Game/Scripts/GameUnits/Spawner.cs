using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform parentTransform;

    private Character[] cachePlayers, cacheBots;
    private Character currentCharacter;

    private Vector3 SPAWN_OFFSET = new Vector3(0f, 1.2f, 0f);

    private void Awake()
    {
        cachePlayers = new Character[DataManager.Instance.PlayerAmount];
        cacheBots = new Character[DataManager.Instance.BotAmount];
    }

    public Character OnInit(Side side, PlayerType type)
    {
        int index = (int)type;

        if (currentCharacter != null) currentCharacter.gameObject.SetActive(false);

        if (cachePlayers[index] == null)
        {
            CharacterData data = DataManager.Instance.GetPlayerData((PlayerType)index);
            currentCharacter = Instantiate(data.Prefab, parentTransform.position + SPAWN_OFFSET, parentTransform.rotation, parentTransform);
            cachePlayers[index] = currentCharacter;
            currentCharacter.InitData(data, side);
        }
        else
        {
            currentCharacter = cachePlayers[index];
            currentCharacter.gameObject.SetActive(true);
        }

        return currentCharacter;
    }

    public Character OnInit(Side side, BotType type)
    {
        int index = (int)type;

        if (currentCharacter != null) currentCharacter.gameObject.SetActive(false);

        if (cacheBots[index] == null)
        {
            CharacterData data = DataManager.Instance.GetBotData(type);
            currentCharacter = Instantiate(data.Prefab, parentTransform.position + SPAWN_OFFSET, parentTransform.rotation, parentTransform);
            cacheBots[index] = currentCharacter;
            currentCharacter.InitData(data, side);
        }
        else
        {
            currentCharacter = cacheBots[index];
            currentCharacter.gameObject.SetActive(true);
        }

        return currentCharacter;
    }
}
