using CP.Furniture;
using UnityEngine;
using UnityEngine.UI;

public class SelectName : MonoBehaviour
{
    private Button Button;

    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnSelected); //��ư�� ��� �߰�
    }
    public void OnSelected()
    {
        TouchInputManager.instance.TrueIsSelect(); //����Ʈ Ʈ��
        TouchInputManager.instance.SetName(gameObject.name);//�̸� ����
    }
}
