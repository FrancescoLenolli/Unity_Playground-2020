using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINotification : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private TextMeshProUGUI label = null;
    [SerializeField] private float animationTime = 1f;

    public void ShowNotification(string text)
    {
        StartCoroutine(ShowNotificationRoutine(text));
    }

    private IEnumerator ShowNotificationRoutine(string text)
    {
        label.text = text;
        canvasGroup.DOFade(1, animationTime);
        transform.DOScale(Vector3.one, animationTime);

        yield return new WaitForSeconds(animationTime * 3);

        Destroy(gameObject);

        yield return null;
    }    
}
