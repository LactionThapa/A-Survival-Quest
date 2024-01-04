using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockItem : Item
{
    [SerializeField] private Weapon glock;
    public override void UseItem(PlayerController playerController)
    {
        playerController.Weapon = glock;
    }
}
