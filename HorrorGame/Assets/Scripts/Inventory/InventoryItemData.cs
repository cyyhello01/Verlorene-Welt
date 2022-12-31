using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryItemData : ScriptableObject
{
   public string id;
   public string displayName;
   public Sprite icon;
   public GameObject prefab;
}
