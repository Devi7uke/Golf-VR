using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallPositionIndicator : MonoBehaviour{
    private Camera cam;
    [SerializeField] private Image image;
    [SerializeField] private float movementDistance = 1f;
    [SerializeField] private float movementSpeed = 0.2f;
    private RectTransform rectTransform;
    private Vector2 initialPosition;
    // Start is called before the first frame update
    void Start(){
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        StartCoroutine(MoveImage());
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update(){
        
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    private IEnumerator MoveImage(){
        while (true){
            Vector2 targetPosition = initialPosition + new Vector2(0f, movementDistance);
            yield return StartCoroutine(MoveToPosition(targetPosition));

            targetPosition = initialPosition;
            yield return StartCoroutine(MoveToPosition(targetPosition));
        }
    }
    private IEnumerator MoveToPosition(Vector2 targetPosition){
        float elapsedTime = 0f;
        Vector2 startPosition = rectTransform.anchoredPosition;

        while (elapsedTime < movementSpeed){
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / movementSpeed);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
    }
}