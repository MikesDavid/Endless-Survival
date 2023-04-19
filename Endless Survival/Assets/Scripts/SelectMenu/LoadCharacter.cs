using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefab;

    public Transform spawnPoint;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject player = characterPrefab[selectedCharacter];

        GameObject playerClone = Instantiate(player, spawnPoint.position, Quaternion.identity);

    }
}
