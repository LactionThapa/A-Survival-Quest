using UnityEngine;

public class GlockAmmo : Item
{
    public int ammoAmount = 15;

    public override void UseItem(PlayerController playerController)
    {
        if (playerController.Weapon.typeOfAmmo == AmmoType.Pistol)
        playerController.Weapon?.addAmmo(ammoAmount);
    }
}
