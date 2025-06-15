using System.Collections;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public TextMeshProUGUI notificationText;
    public float displayDuration = 2f;

    private Coroutine currentCoroutine;

    public void ShowMessage(string message)
    {
        if (notificationText == null)
        {
            Debug.LogWarning("Chưa gán Text thông báo!");
            return;
        }

        notificationText.text = message;
        notificationText.gameObject.SetActive(true);

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        notificationText.gameObject.SetActive(false);
    }
}
