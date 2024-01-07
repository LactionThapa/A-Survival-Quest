using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemiesRemaining : MonoBehaviour
{
    public TextMeshProUGUI zombieCountText;
    public string zombieTag = "Zombie";

    void Update()
    {
        // Find all GameObjects with the specified tag
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("ZombieEnemies");

        // Update the Text component with the remaining count
        zombieCountText.text = "Zombie Enemies Remaining: " + zombies.Length;
    }
}
