using UnityEngine;

public class ItemUser
{
    public void Use(string className, string methodName)
    {
        System.Type.GetType(className).GetMethod(methodName).Invoke(null,null);
    }
}
