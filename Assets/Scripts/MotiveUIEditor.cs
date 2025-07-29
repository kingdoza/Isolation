using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CustomEditor(typeof(Motivation))]
public class MotiveUIEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Motivation motivation = (Motivation)target;

        if (GUILayout.Button("���� �ڵ� ����"))
        {
            GenerateSlots(motivation);
        }
    }

    private void GenerateSlots(Motivation motivation)
    {
        if (motivation.slotContainer == null || motivation.evidenceSlotPrefab == null)
        {
            Debug.LogWarning("slotContainer �Ǵ� evidenceSlotPrefab�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ���� ����
        foreach (Transform child in motivation.slotContainer.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }

        // Evidence > itemNames �� ���� ���� ����
        foreach (var evidence in motivation.evidences)
        {
            foreach (var itemName in evidence.itemNames)
            {
                GameObject slot = (GameObject)PrefabUtility.InstantiatePrefab(motivation.evidenceSlotPrefab, motivation.slotContainer.transform);
                slot.name = $"Slot_{itemName}";

                // ���Կ� �ؽ�Ʈ �� ���� ����
                var text = slot.GetComponentInChildren<UnityEngine.UI.Text>();
                if (text != null)
                    text.text = itemName;
            }
        }

        Debug.Log("���� ���� �Ϸ�!");
    }
}
