using UnityEngine;

public class FurnitureSelector : MonoBehaviour
{
    public GameObject uiPanel;  // 움직임, 회전, 크기 조절 UI 패널

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("ARObject"))  // AR 오브젝트 태그 확인
                {
                    ActivateUIPanel(hit.transform.gameObject);
                }
            }
        }
    }

    void ActivateUIPanel(GameObject selectedObject)
    {
        uiPanel.SetActive(true);
        uiPanel.GetComponent<UIPanelController>().SetTargetObject(selectedObject);
    }
}