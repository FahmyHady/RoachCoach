using RoachCoach.Game;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoachCoach
{
    public class UpgradeUIHandle : MonoBehaviour, IRoachCoachGameAnyWalletAddedListener
    {
        ShopUpgradeData myData;
        [SerializeField] Button myButton;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] TextMeshProUGUI price;
        Entity dummy;
        public void Init(ShopUpgradeData myData, UnityAction myButtonAction)
        {
            this.myData = myData;
            dummy = GameContext.Instance.CreateEntity();
            dummy.AddAnyWalletAddedListener(this);
            myButton.onClick.AddListener(myButtonAction);
            Update();

        }
        private void OnDestroy()
        {
            dummy.RemoveAnyWalletAddedListener(this);
            
        }
        public void OnAnyWalletAdded(Entity entity, int value)
        {
            if (value < myData.cost)
                myButton.interactable = false;
            else
                myButton.interactable = true;
        }

        internal void Update()
        {
            description.text = myData.description;
            price.text = $"Cost\n<color=green>{myData.cost}$</color>";
        }
    }
}
