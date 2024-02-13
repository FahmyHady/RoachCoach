using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoachCoach
{
    internal class UpgradesUIManager : MonoBehaviour
    {
        public static UnityEvent<ShopUpgradeData> OnUpgradeRequested = new UnityEvent<ShopUpgradeData>();
        [SerializeField] UpgradeUIHandle upgradeObjectPrefab;
        [SerializeField] RectTransform upgradesParent;
        [SerializeField] GameObject content;
        [SerializeField] Button menuButton;
        [SerializeField] Button closeButton;
        Dictionary<ShopUpgradeData, UpgradeUIHandle> dataAndUIRepresentation = new Dictionary<ShopUpgradeData, UpgradeUIHandle>();
        private void Awake()
        {
            menuButton.onClick.AddListener(OpenUpgradeMenu);
            closeButton.onClick.AddListener(CloseUpgradeMenu);
        }
        private void OnEnable()
        {
            ShopUpgradesMonobehaviour.OnUpgradeChanged.AddListener(UpgradeChanged);
            ShopUpgradesMonobehaviour.OnUpgradeAdded.AddListener(UpgradeAdded);
            ShopUpgradesMonobehaviour.OnUpgradeRemoved.AddListener(UpgradeRemoved);
        }

        private void OnDisable()
        {
            ShopUpgradesMonobehaviour.OnUpgradeChanged.RemoveListener(UpgradeChanged);
            ShopUpgradesMonobehaviour.OnUpgradeAdded.RemoveListener(UpgradeAdded);
            ShopUpgradesMonobehaviour.OnUpgradeRemoved.RemoveListener(UpgradeRemoved);
        }
        private void UpgradeAdded(ShopUpgradeData arg0)
        {
            var uiHandle = Instantiate(upgradeObjectPrefab, upgradesParent);
            uiHandle.Init(arg0, () => OnUpgradeRequested?.Invoke(arg0));
            dataAndUIRepresentation.Add(arg0, uiHandle);
        }

        private void UpgradeRemoved(ShopUpgradeData arg0)
        {
            var uiGameobject = dataAndUIRepresentation[arg0].gameObject;
            dataAndUIRepresentation.Remove(arg0);
            Destroy(uiGameobject);
        }
        private void UpgradeChanged(ShopUpgradeData arg0)
        {
            dataAndUIRepresentation[arg0].Update();
        }
        public void OpenUpgradeMenu() => content.SetActive(true);
        public void CloseUpgradeMenu() => content.SetActive(false);
    }
}
