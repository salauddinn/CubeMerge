using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction onPointerDownEvent;
    public UnityAction<float> onPointerDragEvent;
    public UnityAction onPointerupEvent;

    private Slider uiSlider;

    private void Awake(){
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData){
        if(onPointerDownEvent != null){
            onPointerDownEvent.Invoke();
        }
        if(onPointerDragEvent != null){
            onPointerDragEvent.Invoke(uiSlider.value);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (onPointerDragEvent != null)
        {
            onPointerDragEvent.Invoke(value);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(onPointerupEvent != null)
        {
            onPointerupEvent.Invoke();
        }
        uiSlider.value = 0f;
    }

    void OnDestroy(){
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
