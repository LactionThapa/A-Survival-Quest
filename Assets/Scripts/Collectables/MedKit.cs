using UnityEngine;

public class Medkit : Item
{
    public int healthRecoveryAmount = 100;

    public override void UseItem(PlayerController playerController)
    {
        playerController.Heal(healthRecoveryAmount);
    }
}
