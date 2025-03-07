using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple player that follows the cursor around
/// Press slowedInput to slow the player down for precision movement
/// </summary>
public class CursorPlayer : MonoBehaviour
{
    [SerializeField] private float regularMaxDistance = 16f;
    [SerializeField] private float slowedMaxDistance = 4f;
    [SerializeField] private KeyCode slowedInput = KeyCode.Mouse0;


    private void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.x /= Screen.width;
        mousePos.y /= Screen.height;

        // You may need to change this value depending on where your scene camera is Z-wise
        mousePos.z = -Camera.main.transform.position.z;

        var mouseWorld = Camera.main.ViewportToWorldPoint(mousePos);

        var maxDistance = Input.GetKey(slowedInput) ? slowedMaxDistance : regularMaxDistance;

        Vector3 difference = mouseWorld - transform.position;
        transform.position = transform.position + difference.normalized * Mathf.Min(difference.magnitude, maxDistance * Time.deltaTime);
    }
}
