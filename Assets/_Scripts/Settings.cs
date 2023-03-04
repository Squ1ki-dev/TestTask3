using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings", fileName = "GameSettings")]
public class Settings : ScriptableObject
{
    public Vector3 minScale;
    public float bulletSpeed;
    public int countObstacles;
}
