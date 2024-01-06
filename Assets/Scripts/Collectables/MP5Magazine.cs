using UnityEngine;

public class MP5Magazine : Item
{
    public int ammoAmount = 30;

    public override void UseItem(PlayerController playerController)
    {
        if (playerController.Weapon.typeOfAmmo == AmmoType.MachineGun)
        playerController.Weapon?.addAmmo(ammoAmount);
    }
}