using System.Collections.Generic;

public static class InstanceTypeDatabase
{
    private static readonly Dictionary<string, InstanceTypeData> database =
        new Dictionary<string, InstanceTypeData>
        {
        };

    public static InstanceTypeData GetInstanceType(string itemTypeID)
    {
        if (string.IsNullOrWhiteSpace(itemTypeID))
            return null;

        if (database.TryGetValue(itemTypeID, out InstanceTypeData data))
            return data;

        return null;
    }
}
