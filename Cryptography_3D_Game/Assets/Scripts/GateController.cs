using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float openHeight = 5f;
    public float openSpeed = 2f;

    private Vector3 initialPosition;
    private Vector3 openPosition;

    private void Start()
    {
        initialPosition = transform.position;
        openPosition = initialPosition + Vector3.up * openHeight;
    }

    public void OpenGate()
    {
        StartCoroutine(MoveGate(openPosition));
    }

    IEnumerator MoveGate(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, openSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
