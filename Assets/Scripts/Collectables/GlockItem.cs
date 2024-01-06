using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockItem : Item
{
    public override void UseItem(PlayerController playerController)
    {
        playerController.glockIsAvailable = true;
        playerController.AddWeapon();
    }
}
