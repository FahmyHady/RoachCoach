using RoachCoach.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RoachCoach
{
    public class Wallet : MonoBehaviour,IRoachCoachGameAnyWalletAddedListener
    {
        [SerializeField] TextMeshProUGUI moneyText;

     
        void Start()
        {
            GameContext.Instance.CreateEntity().AddAnyWalletAddedListener(this);
        }
        public void OnAnyWalletAdded(Entity entity, int value)
        {
            moneyText.text = value.ToString() + " $";
        }


    }
}
