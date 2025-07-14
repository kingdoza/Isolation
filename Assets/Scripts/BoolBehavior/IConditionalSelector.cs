using UnityEngine;

public interface IConditionalSelector
{
    bool Condition { get; }
    Object TrueObject { get; }
    Object FalseObject { get; }
    Object GetConditionalObject(bool status) => Condition ? TrueObject : FalseObject;
}
