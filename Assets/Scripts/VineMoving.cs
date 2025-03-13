using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class VineMoving : MonoBehaviour
{
    // The original position of the vine
    private Vector3 originalPosition;

    // Distance to move (1 unit in the left direction)
    private float moveDistance = 1f;

    // Time it takes to move the vine to the left and back
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Store the original position of the vine
        originalPosition = transform.position;

        // Start the vine movement coroutine
        StartCoroutine(MoveVine());
    }

    // Coroutine to move the vine left and then back
    IEnumerator MoveVine()
    {
        // Move to the left
        Vector3 targetPosition = originalPosition + Vector3.left * moveDistance;

        // Move to the left position
        float elapsedTime = 0f;
        while (elapsedTime < moveSpeed)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }
        transform.position = targetPosition;

        // Wait for a moment before returning to the original position
        yield return new WaitForSeconds(1f);  // Wait for 1 second before moving back

        // Move back to the original position
        elapsedTime = 0f;
        while (elapsedTime < moveSpeed)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }
        transform.position = originalPosition;

        // Restart the cycle (repeat)
        StartCoroutine(MoveVine());
    }
}