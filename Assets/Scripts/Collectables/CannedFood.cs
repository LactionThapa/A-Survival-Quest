using UnityEngine;

public class CannedFood : Item
{
    public int healthRecoveryAmount = 50;

    public override void UseItem(PlayerController playerController)
    {
        playerController.Heal(healthRecoveryAmount);
    }
}
