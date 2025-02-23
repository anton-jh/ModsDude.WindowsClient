namespace ModsDude.WindowsClient.Model.GameAdapters;

public interface IGameAdapter
{
    GameAdapterDescriptor Descriptor { get; }
    bool HasModAdapter { get; }
    bool HasSavegameAdapter { get; }
    object GetBaseConfigurationTemplate { get; }
    object GetInstanceConfigurationTemplate { get; }
}

public abstract class GameAdapterBase<TBaseConfig, TInstanceConfig> : IGameAdapter
{
    public abstract GameAdapterDescriptor Descriptor { get; }

    public bool HasModAdapter => ModAdapter is not null;

    public bool HasSavegameAdapter => SavegameAdapter is not null;

    public abstract IModAdapter<TBaseConfig, TInstanceConfig>? ModAdapter { get; }
    public abstract ISavegameAdapter<TBaseConfig, TInstanceConfig>? SavegameAdapter { get; }
    public abstract object GetBaseConfigurationTemplate { get; }
    public abstract object GetInstanceConfigurationTemplate { get; }
}

public interface IModAdapter<TBaseConfig, TInstanceConfig>
{

}

public interface ISavegameAdapter<TBaseConfig, TInstanceConfig>
{

}
