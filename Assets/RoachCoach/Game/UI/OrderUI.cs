using Entitas;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoachCoach
{
    public class OrderUI : Visual, IRoachCoachGameTacoAddedListener, IRoachCoachGameSodaAddedListener
    {
        Game.Entity linkedCustomerEntity;
        [SerializeField] TextMeshProUGUI commodityCount;
        [SerializeField] Image commodityIcon;
        [SerializeField] Sprite sodaSprite;
        [SerializeField] Sprite tacoSprite;
        int orderValue = 0;
        public override void Link(Entity entity)
        {
            base.Link(entity);
            linkedCustomerEntity = linkedEntity.GetRelatedCustomer().RelatedCustomer;
            linkedCustomerEntity.AddSodaAddedListener(this);
            linkedCustomerEntity.AddTacoAddedListener(this);
            Init();
        }
        void Init()
        {
            var commodity = GameContext.Instance.GetCommodityTypeAndValueRelatedToEntity(linkedEntity);
            orderValue = commodity.value;
            switch (commodity.type)
            {
                case CommodityType.Taco:
                    commodityIcon.sprite = tacoSprite;
                    break;
                case CommodityType.Soda:
                    commodityIcon.sprite = sodaSprite;
                    break;
            }
            UpdateText();
        }

     

        public void OnSodaAdded(Game.Entity entity, int value)
        {
            orderValue--;
            UpdateText();
        }

        public void OnTacoAdded(Game.Entity entity, int value)
        {
            orderValue--;
            UpdateText();
        }
        private void UpdateText()
        {
            commodityCount.text = orderValue.ToString();
        }
    }
}
