using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Vector3", fileName = "New Vector3")]
public class Vector3Value : ScriptableObject
{
    [SerializeField] private Vector3 value;

    [field:NonSerialized] public Vector3 Value { get; set; }

    private void OnEnable()
    {
        Value = value;
    }
}
