using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP5Item : Item
{
    [SerializeField] private Weapon mp5;
    public override void UseItem(PlayerController playerController)
    {
        playerController.Weapon = mp5;
    }
}
