using UnityEngine;
using static GameData;

public static class EtcUtils
{
    public static Vector2 GetCenter(this Texture2D texture)
    {
        return new Vector2(texture.width / 2, texture.height / 2);
    }



    public static void SetCursorTexture(Texture2D texture, Vector2 hotspot)
    {
        Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
    }



    public static void SetCursorTexture(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }



    public static void SetCursorTexture()
    {
        Cursor.SetCursor(NoneStuffData.CursorTexture, Vector2.zero, CursorMode.Auto);
    }



    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



    public static void PrintErrorLog(GameObject gameObject, Error errorType)
    {
        Debug.Log(errorType.ToString() + " : " + gameObject.name);
    }



    public static void PrintErrorLog(string text, Error errorType)
    {
        Debug.Log(errorType.ToString() + " : " + text);
    }



    public static T LoadResource<T>(ref T cache, string path) where T : Object
    {
        if (cache != null) 
            return cache;
        cache = Resources.Load<T>(path);
        if (cache == null)
        {
            PrintErrorLog(path, Error.ResourceLoadFail);
        }
        return cache;
    }
}



public class Error
{
    private string Code { get; }
    private string Message { get; }

    private Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public override string ToString() => $"[{Code}] {Message}";

    public static readonly Error NullComp = new Error("NullComp", "필수 컴포넌트가 누락되었습니다.");
    public static readonly Error MissingRef = new Error("MissingRef", "참조가 없습니다.");
    public static readonly Error ResourceLoadFail = new Error("ResourceLoadFail", "리소스를 로드하는 데 실패했습니다.");
}
