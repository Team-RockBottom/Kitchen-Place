using System;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSlotDataModel : MonoBehaviour
{
    [Serializable]
    public class FurnitureSlotData
    {
        public int furnitureIndex;
        public int furnitureCount;
    }

    [Serializable]
    public class BasketData
    {
        public BasketData(int capacity)
        {
            slotDataList = new List<FurnitureSlotData>(capacity);
        }

        public List<FurnitureSlotData> slotDataList;
    }
}