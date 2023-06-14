using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public int BulletCount;
    public float Damage;
    public bool isAuto;
    public float ReturnTime;
    public Vector3 RecoilAmount;
}
