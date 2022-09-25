using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        Debug.Log("Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
