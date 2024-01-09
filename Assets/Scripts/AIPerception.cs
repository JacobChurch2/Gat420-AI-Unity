using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPerception : MonoBehaviour
{
    [SerializeField] string tagName = "";
    [SerializeField] float distance;
    [SerializeField] float maxAngle = 45;
    public string Tagname { get { return tagName; } }
    public float Distance { get { return distance; } }
    public float MaxAngle { get {  return maxAngle; } }

    public abstract GameObject[] GetGameObjects();
}
