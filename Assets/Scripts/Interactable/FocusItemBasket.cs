using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class FocusItemBasket : FocusItem
{
    protected override AudioClip ClikBase => SFXClips.familiyPhoto_BasketFocus;
}

