using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ASD/Item", order = 999)]
public class Item_SO : ScriptableObject
{
    [SerializeField, TextArea(1, 30)] protected string toolTip; // protected, use ToolTip()

    [SerializeField] private Sprite sprite;                     // sprite of the item
    public Sprite Sprite { get => sprite; set => sprite = value; }

    [SerializeField] private Sprite selectedSprite;                     // sprite of the item
    public Sprite SelectedSprite { get => selectedSprite; set => selectedSprite = value; }

    [SerializeField] private GameObject itemPrefab;             // Spawned by the toolbar
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }

    [Tooltip("Can this item be stored in toolbar for later use? If no, the item will be thrown away after switching items.")]
    [SerializeField] private bool storeInToolBar = true;
    public bool StoreInToolBar { get => storeInToolBar; set => storeInToolBar = value; }

}