namespace API.Modules.Base;

public interface IModule
{
    ModuleConfiguration Configuration { get; }

    void Map(WebApplication app);
}

public abstract class Module : IModule
{
    public abstract ModuleConfiguration Configuration { get; }
   public abstract void Map(WebApplication app);
}
