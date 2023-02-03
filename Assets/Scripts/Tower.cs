using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Level {get; set;}
    public float Damage { get; set; }
    public float ShootSpeed { get; set; }
    public float ShootRange { get; set; }
    public CardHand TowerCardResult { get; set; }
}
