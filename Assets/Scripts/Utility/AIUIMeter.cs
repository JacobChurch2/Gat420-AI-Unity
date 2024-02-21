using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Define the AIUIMeter class, extending MonoBehaviour
public class AIUIMeter : MonoBehaviour
{
    // Serialized fields for easy access in the Unity Editor
    [SerializeField] TMP_Text label;    // Text component for displaying labels
    [SerializeField] Slider slider;      // Slider component for visualizing a value
    [SerializeField] Image image;        // Image component for additional visual elements

    // Property for setting the position of the UI element in world space
    public Vector3 position
    {
        set
        {
            // Draw a debug line from the given position upward for visualization
            Debug.DrawLine(value, value + Vector3.up * 3);

            // Convert world position to viewport coordinates
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(value);

            // Set the anchors of the RectTransform to position the UI element
            GetComponent<RectTransform>().anchorMin = viewportPoint;
            GetComponent<RectTransform>().anchorMax = viewportPoint;
        }
    }

    // Property for setting the value of the slider
    public float value
    {
        set
        {
            // Set the value of the slider component
            slider.value = value;
        }
    }

    // Property for setting the text of the label
    public string text
    {
        set
        {
            // Set the text of the label component
            label.text = value;
        }
    }

    // Property for toggling the visibility of the UI element
    public bool visible
    {
        set
        {
            // Set the visibility of the entire UI element
            gameObject.SetActive(value);
        }
    }

    // Property for setting the alpha (transparency) of the image
    public float alpha
    {
        set
        {
            // Get the current color of the image
            Color color = image.color;

            // Set the alpha value of the color
            color.a = value;

            // Apply the modified color back to the image
            image.color = color;
        }
    }
}