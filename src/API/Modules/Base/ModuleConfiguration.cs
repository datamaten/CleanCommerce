namespace API.Modules.Base;

public record ModuleConfiguration(string GroupName, string Endpoint, string[] Tags)
{
    public ModuleConfiguration(string groupName)
        : this(groupName, groupName.ToLower(), [groupName])
    {
    }

    public ModuleConfiguration(string groupName, string endpoint)
        : this(groupName, endpoint, [groupName])
    {
    }
}
