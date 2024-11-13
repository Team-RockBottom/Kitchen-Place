using CP.Furniture;
using UnityEngine;
using UnityEngine.UI;

public class SelectName : MonoBehaviour
{
    private Button Button;

    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(OnSelected); //버튼에 기능 추가
    }
    public void OnSelected()
    {
        TouchInputManager.instance.TrueIsSelect(); //셀렉트 트루
        TouchInputManager.instance.SetName(gameObject.name);//이름 전송
    }
}
