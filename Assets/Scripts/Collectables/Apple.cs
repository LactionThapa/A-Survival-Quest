using UnityEngine;

public class Apple : Item
{
    public int healthRecoveryAmount = 15;

    public override void UseItem(PlayerController playerController)
    {
        playerController.Heal(healthRecoveryAmount);
    }
}
