using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Motivation", menuName = "Scriptable Objects/Motivation")]
public class Motivation : ScriptableObject
{
    public EndingType type;
    public string title;
    public string subtitle;
    public List<Evidence> evidences;

#if UNITY_EDITOR
    public GameObject slotContainer;
    public GameObject evidenceSlotPrefab;
#endif
}



[Serializable]
public class Evidence
{
    public string title;
    public List<string> itemNames;
}



public enum EndingType
{
    Bad, Happy
}
