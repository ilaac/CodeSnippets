using UnityEngine;

public class ViewBobbing : MonoBehaviour
{
    public bool bobbingActive;
    public int bobbingInt;
    public float bobbingSpeed;
    public float bobbingAmount;
    public float midpoint;
    public float bobSmoothness;
    public float resetSmoothness;

    private float bobbingTimer = 0.0f;
    private float resetTimer = 0.0f;
    private Vector3 originalPosition;

    // Reference to the PlayerMovement script
    public PlayerMovement playerMovement;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        bobbingActive = ControlsManager.Instance.isHeadBobbing;
        if (bobbingInt == 1)
        {
            bobbingActive = true;
        }
        if (bobbingActive)
        {
            float waveslice = 0.0f;

            // Get the player's speed from the PlayerMovement script
            float playerSpeed = playerMovement.GetCurrentSpeed();

            // Adjust bobbing speed based on player's speed
            float adjustedBobbingSpeed = bobbingSpeed * playerSpeed;

            // Calculate bobbing using adjusted speed
            waveslice = Mathf.Sin(bobbingTimer);
            bobbingTimer = bobbingTimer + adjustedBobbingSpeed * Time.deltaTime;
            if (bobbingTimer > Mathf.PI * 2)
            {
                bobbingTimer = bobbingTimer - (Mathf.PI * 2);
            }

            float targetBobbingPosition = midpoint + waveslice * bobbingAmount;

            // Check if the player is moving
            if (playerSpeed > 0.75f)
            {
                // Smoothly interpolate towards the target position
                Vector3 newPosition = new Vector3(transform.localPosition.x, targetBobbingPosition, transform.localPosition.z);
                transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * bobSmoothness);

                // Reset the reset timer
                resetTimer = 0.0f;
            }
            else
            {
                // If the player is not moving, gradually move back to the original position
                resetTimer += Time.deltaTime;
                float t = Mathf.Clamp01(resetTimer / bobbingSpeed); // Adjust bobbingSpeed as needed
                Vector3 newPosition = Vector3.Lerp(transform.localPosition, originalPosition, t);
                transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime * resetSmoothness);
            }
        }
        
    }
}
