using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weapon.asset", menuName ="Scriptable Objects/Weapon Type")]
public class WeaponType : ScriptableObject
{
    public string weaponName;
    public int weaponId;
    public bool isMelee;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public AudioClip shotClip, reloadClip;
    public Sprite weaponSprite;
}
