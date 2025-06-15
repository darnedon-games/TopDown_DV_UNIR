using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject inventoryFrame;
    [SerializeField] private Button[] buttons;

    private int availableItems = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => clickedButton(buttonIndex));
        }
    }

    private void clickedButton(int buttonIndex)
    {
        Debug.Log("Botón clickado " + buttonIndex);
    }

    public void NewItem(ItemSO itemData)
    {
        // 1. Al tener nuevo item, activo un botón de mi inventario
        buttons[availableItems].gameObject.SetActive(true);
        // 2. Alimento al botón con los datos alojados en el SO
        buttons[availableItems].GetComponent<Image>().sprite = itemData.icon;
        availableItems++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryFrame.SetActive(!inventoryFrame.activeSelf);
        }
    }
}
