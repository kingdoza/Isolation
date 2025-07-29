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

        if (GUILayout.Button("슬롯 자동 생성"))
        {
            GenerateSlots(motivation);
        }
    }

    private void GenerateSlots(Motivation motivation)
    {
        if (motivation.slotContainer == null || motivation.evidenceSlotPrefab == null)
        {
            Debug.LogWarning("slotContainer 또는 evidenceSlotPrefab이 설정되지 않았습니다.");
            return;
        }

        // 기존 슬롯 제거
        foreach (Transform child in motivation.slotContainer.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }

        // Evidence > itemNames 에 따라 슬롯 생성
        foreach (var evidence in motivation.evidences)
        {
            foreach (var itemName in evidence.itemNames)
            {
                GameObject slot = (GameObject)PrefabUtility.InstantiatePrefab(motivation.evidenceSlotPrefab, motivation.slotContainer.transform);
                slot.name = $"Slot_{itemName}";

                // 슬롯에 텍스트 등 설정 가능
                var text = slot.GetComponentInChildren<UnityEngine.UI.Text>();
                if (text != null)
                    text.text = itemName;
            }
        }

        Debug.Log("슬롯 생성 완료!");
    }
}
