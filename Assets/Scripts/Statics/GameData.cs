using UnityEngine;
using static EtcUtils;

public static class GameData
{
    private static StuffTypeData _noneStuffData;
    public static StuffTypeData NoneStuffData => LoadResource(ref _noneStuffData, "Stuffs/SO_None");

    private static StuffTypeData _dragStuffData;
    public static StuffTypeData DragStuffData => LoadResource(ref _dragStuffData, "Stuffs/SO_Drag");

    private static StuffTypeData _zoomStuffData;
    public static StuffTypeData ZoomStuffData => LoadResource(ref _zoomStuffData, "Stuffs/SO_Zoom");

    private static StuffTypeData _itemStuffData;
    public static StuffTypeData ItemStuffData => LoadResource(ref _itemStuffData, "Stuffs/SO_Item");

    private static StuffTypeData _focusStuffData;
    public static StuffTypeData FocusStuffData => LoadResource(ref _focusStuffData, "Stuffs/SO_Focus");

    private static StuffTypeData _dialogueStuffData;
    public static StuffTypeData DialogueStuffData => LoadResource(ref _dialogueStuffData, "Stuffs/SO_Dialogue");

    private static StuffTypeData _evidenceStuffData;
    public static StuffTypeData EvidenceStuffData => LoadResource(ref _evidenceStuffData, "Stuffs/SO_Evidence");

    private static StuffTypeData _lightSwitchStuffData;
    public static StuffTypeData LightSwitchStuffData => LoadResource(ref _lightSwitchStuffData, "Stuffs/SO_LightSwitch");
}
