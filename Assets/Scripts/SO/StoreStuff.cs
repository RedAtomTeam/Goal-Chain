using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreStuff", menuName = "Scriptable Objects/StoreStuff")]
public class StoreStuff : ScriptableObject
{
    public string stuffName;
    public bool isBuy;
    public int price;
    public bool isSelected;
    public List<StoreStuff> siblings;
    public Sprite _sprite;
}
