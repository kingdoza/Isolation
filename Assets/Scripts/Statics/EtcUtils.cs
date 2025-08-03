using Unity.Collections;
using UnityEditor.PackageManager;
using UnityEngine;

public static class EtcUtils
{
    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



    public static void PrintErrorLog(GameObject gameObject, Error errorType)
    {
        Debug.Log(errorType.ToString() + " : " + gameObject.name);
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
}
