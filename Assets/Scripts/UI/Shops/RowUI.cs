using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameField;
        [SerializeField] Image iconField;
        [SerializeField] TextMeshProUGUI availabilityField;
        [SerializeField] TextMeshProUGUI priceField;
        [SerializeField] TextMeshProUGUI quantityField;

        Shop currentShop = null;
        ShopItem item = null;

        public void SetUp(Shop currentShop, ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;
            nameField.text = item.GetName();
            iconField.sprite = item.GetIcon();
            availabilityField.text = $"{item.GetAvailability()}";
            priceField.text = $"${item.GetPrice()}";
            quantityField.text = $"{item.GetQuantity()}";
        }

        public void Add()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);
        }

        public void Remove()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);
        }
    }
}