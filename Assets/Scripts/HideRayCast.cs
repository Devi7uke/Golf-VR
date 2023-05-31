using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideRayCast : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private XRInteractorLineVisual lienVisualL, lienVisualR;
    private XRGrabInteractable grabInteractable;
    // Start is called before the first frame update
    void Start() {
        lienVisualL = leftHand.GetComponent<XRInteractorLineVisual>();
        lienVisualR = rightHand.GetComponent<XRInteractorLineVisual>();
        grabInteractable = GameObject.FindGameObjectWithTag("Club").GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update() {
        lienVisualL.enabled = !grabInteractable.isSelected;
        lienVisualR.enabled = !grabInteractable.isSelected;
    }
}