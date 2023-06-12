using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    string Name;
    GameObject Prefab;
    float Damage;
    float ReturnTime;
    Vector3 RecoilAmount;
}
