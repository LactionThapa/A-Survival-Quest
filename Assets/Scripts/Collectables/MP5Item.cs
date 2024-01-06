using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP5Item : Item
{
    public override void UseItem(PlayerController playerController)
    {
        playerController.mp5IsAvailable = true;
        playerController.AddWeapon();
    }
}
