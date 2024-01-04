using UnityEngine;

public class Orange : Item
{
    public int healthRecoveryAmount = 25;

    public override void UseItem(PlayerController playerController)
    {
        playerController.Heal(healthRecoveryAmount);
    }
}
