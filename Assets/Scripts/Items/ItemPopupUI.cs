using UnityEngine;
using TMPro;
using System.Collections;

public class ItemPopupUI : MonoBehaviour
{
    public static ItemPopupUI Instance;

    public TextMeshProUGUI text;
    public float displayTime = 2.5f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(string itemName, string description)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        gameObject.SetActive(true);
        text.text = $"<b>{itemName}</b>\n{description}";
        currentRoutine = StartCoroutine(HideAfterTime());
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        gameObject.SetActive(false);
    }
}

