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
        GUILayout.Label("Motivation 슬롯 생성기", EditorStyles.boldLabel);

        motivationAsset = (Motivation)EditorGUILayout.ObjectField("Motivation Asset", motivationAsset, typeof(Motivation), false);
        slotContainer = (GameObject)EditorGUILayout.ObjectField("Slot Container", slotContainer, typeof(GameObject), true);
        AutoAssignUIDependencies();

        if (GUILayout.Button("슬롯 생성"))
        {
            if (motivationAsset == null || slotContainer == null || slotPrefab == null)
            {
                EditorUtility.DisplayDialog("입력 누락", "필수 필드를 모두 지정해주세요.", "확인");
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
        // uiData가 이미 할당되었는지 확인
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
        // 1. slotContainer 하위의 기존 EvidenceParent(빈 부모)들 수집
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

            // 2. Evidence 부모 오브젝트 재사용 또는 생성
            if (!existingEvidenceParents.TryGetValue(evidenceParentName, out evidenceParentGO))
            {
                evidenceParentGO = new GameObject(evidenceParentName, typeof(RectTransform));
                evidenceParentGO.transform.SetParent(slotContainer.transform, false);
            }
            else
            {
                existingEvidenceParents.Remove(evidenceParentName);
            }

            // 4. item 슬롯 관리
            Dictionary<string, GameObject> existingItemSlots = new Dictionary<string, GameObject>();
            foreach (Transform child in evidenceParentGO.transform)
            {
                if (child.name.StartsWith("Evidence")) continue; // 증거 제목 노드는 제외
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

                    // 슬롯 텍스트 설정
                    var text = slotGO.GetComponentInChildren<Text>();
                    if (text != null)
                        text.text = itemName;

                    // 슬롯 크기 설정
                    var rect = slotGO.GetComponent<RectTransform>();
                    if (rect != null)
                        rect.sizeDelta = Vector2.one * generationFormat.itemSlotSize;
                }
            }

            // 3. 증거 제목 노드 (Evidence_제목) 재사용 또는 생성
            string evidenceNodeName = $"Evidence_{evidence.title}";
            Transform evidenceNodeTransform = evidenceParentGO.transform.Find(evidenceNodeName);
            GameObject evidenceNode;
            if (evidenceNodeTransform == null)
            {
                evidenceNode = (GameObject)PrefabUtility.InstantiatePrefab(slotPrefab, evidenceParentGO.transform);
                evidenceNode.name = evidenceNodeName;

                // 텍스트 설정
                var text = evidenceNode.GetComponentInChildren<Text>();
                if (text != null)
                    text.text = evidence.title;

                // 크기 설정
                var rect = evidenceNode.GetComponent<RectTransform>();
                if (rect != null)
                    rect.sizeDelta = Vector2.one * generationFormat.clueSlotSize;
            }
            else
            {
                evidenceNode = evidenceNodeTransform.gameObject;
            }

            // 5. 사용하지 않는 슬롯 삭제
            foreach (var unusedSlot in existingItemSlots.Values)
            {
                DestroyImmediate(unusedSlot);
            }

            evidenceIndex++;
        }

        // 6. slotContainer에 남은 오래된 Evidence 부모 오브젝트 제거
        foreach (var unusedParent in existingEvidenceParents.Values)
        {
            DestroyImmediate(unusedParent);
        }

        if (slotPrefab != null)
        {
            // 이미 Final_Slot 이름의 자식이 존재하는지 확인
            bool slotExists = false;
            foreach (Transform child in slotContainer.transform)
            {
                if (child.name.StartsWith("Final_"))
                {
                    slotExists = true;
                    break;
                }
            }

            // 없을 때만 생성
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
        Debug.Log("슬롯 생성 완료");
    }


}
