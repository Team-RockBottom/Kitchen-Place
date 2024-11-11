using UnityEngine;

public class MoveMode : MonoBehaviour
{
    private GameObject targetObject;
    private bool isMoving = false;

    public void Activate(GameObject obj)
    {
        targetObject = obj;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(targetObject.transform.position).z));
            targetObject.transform.position = new Vector3(touchPosition.x, targetObject.transform.position.y, touchPosition.z);

            if (touch.phase == TouchPhase.Ended)
                isMoving = false;
        }
    }
}
