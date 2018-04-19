using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private Image bgImg;
	private Image jstImg;
	private Vector3 inputVector;

    public bool move = false;
    public bool shoot = false;

    public AudioClip audioWalk;
    public AudioSource audioSource;
    void Start()	
	{
        
		bgImg = transform.GetComponent<Image> ();
		jstImg = transform.GetChild (0).GetComponent<Image> ();

	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
        shoot = true;
        audioSource.clip = audioWalk;
        audioSource.Play();
    }

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		jstImg.rectTransform.anchoredPosition = Vector3.zero;
        move = false;
        shoot = false;
        audioSource.Stop();
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos = Vector2.zero;
		if (bgImg != null) {
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
				pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
				pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
				inputVector = new Vector3 (pos.x*2 - 1 , 0, pos.y*2 -1); 
				inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

				//move jst image
				jstImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x/2),
					inputVector.z * (bgImg.rectTransform.sizeDelta.y/2),0);

                move = true;
                
			}
            
        }
	}

	public float Vertical()
	{
		if (inputVector.z != 0)
			return inputVector.z;
		else
			return Input.GetAxis ("Vertical");
	}
	public float Horizontal()
	{
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis ("Horizontal");
	}

}
