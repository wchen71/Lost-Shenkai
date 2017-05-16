using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VrUiButton : MonoBehaviour {

	protected BoxCollider myCol;
	protected RectTransform rt;
	protected Button myButton;

    private AudioSource buttonSFXSource;
    private AudioClip hoverButtonSound;
    private AudioClip selectButtonSound;

	protected virtual void Awake () {
        buttonSFXSource = GetComponent<AudioSource>();
        hoverButtonSound = (AudioClip)Resources.Load("Button Hover Sound");
        selectButtonSound = (AudioClip)Resources.Load("Button Click Sound");

        myCol = GetComponent<BoxCollider> ();
		rt = GetComponent<RectTransform> ();
		myButton = GetComponent<Button> ();
		if (rt.localScale == Vector3.one) {
			CreateAndResizeCollider ();
		} else {
			Debug.LogError ("VR UI Buttons should never have a scale that is not (1, 1, 1)");
		}
	}

	void CreateAndResizeCollider () {
		if (myCol == null) {
			myCol = gameObject.AddComponent<BoxCollider> ();
		}
		myCol.size = new Vector3 (rt.sizeDelta.x,
			rt.sizeDelta.y,
			10);
	}

	public void Highlight () {
		myButton.interactable = true;
        //play highlight soundeffect
        buttonSFXSource.PlayOneShot(hoverButtonSound);
    }


    public void Unhighlight () {
		myButton.interactable = false;
        //play unhighlight soundeffect (there currently is none)
        //buttonSFXSource.PlayOneShot(hoverButtonSound);
    }

	public void Select () {
		buttonSFXSource.PlayOneShot(selectButtonSound);
		myButton.onClick.Invoke ();
        //play click button sound
    }
}
