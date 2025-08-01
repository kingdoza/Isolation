using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MotiveUIEditorWindow : EditorWindow
{
    private Motivation motivationAsset;
    private GameObject slotContainer;
    private GameObject slotPrefab;
    private MotiveUIData generationFormat;

    private GameObject finalSlot;
    private List<GameObject> evidenceSlots;
    private List<GameObject> itemSlots;

    [MenuItem("Tools/Motivation Slot Generator")]
    public static void ShowWindow()
    {
        GetWindow<MotiveUIEditorWindow>("Motivation Slot Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Motivation ���� ������", EditorStyles.boldLabel);

        motivationAsset = (Motivation)EditorGUILayout.ObjectField("Motivation Asset", motivationAsset, typeof(Motivation), false);
        slotContainer = (GameObject)EditorGUILayout.ObjectField("Slot Container", slotContainer, typeof(GameObject), true);
        AutoAssignUIDependencies();

        if (GUILayout.Button("���� ����"))
        {
            if (motivationAsset == null || slotContainer == null || slotPrefab == null)
            {
                EditorUtility.DisplayDialog("�Է� ����", "�ʼ� �ʵ带 ��� �������ּ���.", "Ȯ��");
                return;
            }

            GenerateSlots();
            GetAllSlots();
            DrawSlotLines();
            ApplyFormatAndComponent();
        }
    }



    private void AutoAssignUIDependencies()
    {
        // uiData�� �̹� �Ҵ�Ǿ����� Ȯ��
        if (generationFormat == null)
        {
            string[] guids = AssetDatabase.FindAssets("t:MotiveUIData");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                generationFormat = AssetDatabase.LoadAssetAtPath<MotiveUIData>(path);
            }
        }

        if (generationFormat != null)
        {
            if (slotPrefab == null)
                slotPrefab = generationFormat.slotPrefab;
        }
    }



    private void GetAllSlots()
    {
        MindTreeUI mindTreeUI = Object.FindAnyObjectByType<MindTreeUI>();
        evidenceSlots = new List<GameObject>();
        itemSlots = new List<GameObject>();

        foreach (Transform child in slotContainer.transform)
        {
            if (child.name.StartsWith("Final_"))
            {
                finalSlot = child.gameObject;
            }
            else if(child.name.StartsWith("Evidence"))
            {
                foreach (Transform node in child)
                {
                    if (node.name.StartsWith("Evidence_"))
                        evidenceSlots.Add(node.gameObject);
                    else if (node.name.StartsWith("Item_"))
                        itemSlots.Add(node.gameObject);
                }
            }
        }
    }



    private void DrawSlotLines()
    {
        foreach(Transform child in slotContainer.transform)
        {
            if(child.name.StartsWith("Evidence"))
            {
                for(int i = 0; i < child.childCount; i++)
                {
                    MotiveSlot currentSlot = child.GetChild(i).GetComponent<MotiveSlot>();
                    GameObject nextSlotObj = (i >= child.childCount - 1) ? finalSlot : child.GetChild(i + 1).gameObject;
                    MotiveSlot nextSlot = nextSlotObj.GetComponent<MotiveSlot>();
                    if(currentSlot == null || nextSlot == null)
                        continue;
                    currentSlot.DrawLineTo(nextSlot, generationFormat);
                }
            }
        }
    }



    private void ApplyFormatAndComponent()
    {
        //MindTreeUI mindTreeUI = Object.FindAnyObjectByType<MindTreeUI>();
        MotiveSlot slot;
        foreach (GameObject itemSlot in itemSlots)
        {
            if (itemSlot.GetComponent<CollectionSlot>() == null)
                itemSlot.AddComponent<CollectionSlot>();
            slot = itemSlot.GetComponent<CollectionSlot>();
            slot.ApplySize(generationFormat.itemSlotSize);
            //mindTreeUI.ItemSlots.Add(slot as CollectionSlot);
        }

        foreach (GameObject evidenceSlot in evidenceSlots)
        {
            if (evidenceSlot.GetComponent<EvidenceSlot>() == null)
                evidenceSlot.AddComponent<EvidenceSlot>();
            slot = evidenceSlot.GetComponent<EvidenceSlot>();
            slot.ApplySize(generationFormat.clueSlotSize);
            //mindTreeUI.EvidenceSlots.Add(slot as EvidenceSlot);
        }

        if (finalSlot.GetComponent<EndingSlot>() == null)
            finalSlot.AddComponent<EndingSlot>();
        slot = finalSlot.GetComponent<EndingSlot>();
        slot.ApplySize(generationFormat.finalSlotSize);
        finalSlot.GetComponent<RectTransform>().localPosition = generationFormat.finalSlotPosition;
        //mindTreeUI.EndingSlot = slot as EndingSlot;
    }



    private void GenerateSlots()
    {
        // 1. slotContainer ������ ���� EvidenceParent(�� �θ�)�� ����
        Dictionary<string, GameObject> existingEvidenceParents = new Dictionary<string, GameObject>();
        foreach (Transform child in slotContainer.transform)
        {
            if(child.name.StartsWith("Evidence"))
                existingEvidenceParents[child.name] = child.gameObject;
        }

        int evidenceIndex = 1;
        foreach (var evidence in motivationAsset.evidences)
        {
            string evidenceParentName = $"Evidence{evidenceIndex}";
            GameObject evidenceParentGO;

            // 2. Evidence �θ� ������Ʈ ���� �Ǵ� ����
            if (!existingEvidenceParents.TryGetValue(evidenceParentName, out evidenceParentGO))
            {
                evidenceParentGO = new GameObject(evidenceParentName, typeof(RectTransform));
                evidenceParentGO.transform.SetParent(slotContainer.transform, false);
            }
            else
            {
                existingEvidenceParents.Remove(evidenceParentName);
            }

            // 4. item ���� ����
            Dictionary<string, GameObject> existingItemSlots = new Dictionary<string, GameObject>();
            foreach (Transform child in evidenceParentGO.transform)
            {
                if (child.name.StartsWith("Evidence")) continue; // ���� ���� ���� ����
                existingItemSlots[child.name] = child.gameObject;
            }

            foreach (var itemName in evidence.itemNames)
            {
                string slotName = $"Item_{itemName}";
                if (existingItemSlots.TryGetValue(slotName, out GameObject slotGO))
                {
                    existingItemSlots.Remove(slotName);
                }
                else
                {
                    slotGO = (GameObject)PrefabUtility.InstantiatePrefab(slotPrefab, evidenceParentGO.transform);
                    slotGO.name = slotName;

                    // ���� �ؽ�Ʈ ����
                    var text = slotGO.GetComponentInChildren<Text>();
                    if (text != null)
                        text.text = itemName;

                    // ���� ũ�� ����
                    var rect = slotGO.GetComponent<RectTransform>();
                    if (rect != null)
                        rect.sizeDelta = Vector2.one * generationFormat.itemSlotSize;
                }
            }

            // 3. ���� ���� ��� (Evidence_����) ���� �Ǵ� ����
            string evidenceNodeName = $"Evidence_{evidence.title}";
            Transform evidenceNodeTransform = evidenceParentGO.transform.Find(evidenceNodeName);
            GameObject evidenceNode;
            if (evidenceNodeTransform == null)
            {
                evidenceNode = (GameObject)PrefabUtility.InstantiatePrefab(slotPrefab, evidenceParentGO.transform);
                evidenceNode.name = evidenceNodeName;

                // �ؽ�Ʈ ����
                var text = evidenceNode.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = evidence.title;

                // ũ�� ����
                var rect = evidenceNode.GetComponent<RectTransform>();
                if (rect != null)
                    rect.sizeDelta = Vector2.one * generationFormat.clueSlotSize;
            }
            else
            {
                evidenceNode = evidenceNodeTransform.gameObject;
            }

            // 5. ������� �ʴ� ���� ����
            foreach (var unusedSlot in existingItemSlots.Values)
            {
                DestroyImmediate(unusedSlot);
            }

            evidenceIndex++;
        }

        // 6. slotContainer�� ���� ������ Evidence �θ� ������Ʈ ����
        foreach (var unusedParent in existingEvidenceParents.Values)
        {
            DestroyImmediate(unusedParent);
        }

        if (slotPrefab != null)
        {
            // �̹� Final_Slot �̸��� �ڽ��� �����ϴ��� Ȯ��
            bool slotExists = false;
            foreach (Transform child in slotContainer.transform)
            {
                if (child.name.StartsWith("Final_"))
                {
                    slotExists = true;
                    break;
                }
            }

            // ���� ���� ����
            if (!slotExists)
            {
                GameObject finalSlotGO = (GameObject)PrefabUtility.InstantiatePrefab(slotPrefab, slotContainer.transform);
                string finalNodeName = $"Final_{motivationAsset.type}";
                finalSlotGO.name = finalNodeName;

                var rect = finalSlotGO.GetComponent<RectTransform>();
                if (rect != null)
                {
                    rect.localPosition = generationFormat.finalSlotPosition;
                    rect.sizeDelta = Vector2.one * generationFormat.finalSlotSize;
                }

                var text = finalSlotGO.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = "Final Slot";
            }
        }

        EditorUtility.SetDirty(slotContainer);
        Debug.Log("���� ���� �Ϸ�");
    }


}
