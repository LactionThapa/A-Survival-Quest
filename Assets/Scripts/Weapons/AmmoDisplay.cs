using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    void Start()
    {
        Weapon.showAmmo += ChangeAmmoText;
    }

    private void ChangeAmmoText(int ammoCount, int totalCount)
    {
        ammoText.text = $"{ammoCount} / {totalCount}";
    }
}
