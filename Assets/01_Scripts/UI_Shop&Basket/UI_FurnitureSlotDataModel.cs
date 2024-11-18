using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_FurnitureSlotDataModel : MonoBehaviour
{
    [Serializable]
    public class UI_FurnitureSlotData
    {
        public int furnitureIndex;
        public int furnitureCount;
    }

    [Serializable]
    public class BasketData
    {
        public BasketData(int capacity)
        {
            slotDataList = new List<UI_FurnitureSlotData>(capacity);
        }

        public List<UI_FurnitureSlotData> slotDataList;
    }
}