using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UI_FurnitureSpecRepository", menuName = "Scriptable Objects/UI_FurnitureSpecRepository")]
public class UI_FurnitureSpecRepository : ScriptableObject
{
    [field: SerializeField] public List<FurnitureSpec> specs;   //��ϵ� ���� ����Ʈ 

    public FurnitureSpec Get(int itemid)
    {
        int index = specs.FindIndex(spec => spec.Index == itemid);

        if (index < 0)
        {
            //���� �޽��� ���� 
            throw new System.Exception($"[{nameof(FurnitureSpec)}] : Not Found. {itemid}.");
        }
        else
        {
            return specs[index];    
        }
    }
}