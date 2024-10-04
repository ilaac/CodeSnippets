using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public RawImage grayOutImage; // Reference to the RawImage for grayed-out effect
    public float countdownDuration = 3.0f; // Adjust this to set the countdown duration in seconds
    public Car playerControl; // Reference to your player control script/component

    private void Start()
    {
        playerControl.enabled = false; // Disable player controls at the start.
        grayOutImage.enabled = true;
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        float timeLeft = countdownDuration;

        while (timeLeft > 0)
        {
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1.0f);
            timeLeft -= 1.0f;
        }

        countdownText.text = "GO!";

        yield return new WaitForSeconds(0.4f);

        countdownText.gameObject.SetActive(false); // Hide the countdown text
        grayOutImage.enabled = false; // Disable the RawImage for grayed-out effect
        playerControl.enabled = true; // Re-enable player controls.
    }
}
