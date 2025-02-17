namespace ModsDude.WindowsClient.Model.GameAdapters;

public interface IGameAdapter
{
    GameAdapterDescriptor Descriptor { get; }
    IModAdapter? ModAdapter { get; }
    ISavegameAdapter? SavegameAdapter { get; }
    bool HasModAdapter => ModAdapter is not null;
    bool HasSavegameAdapter => SavegameAdapter is not null;
}

public interface IModAdapter
{

}

public interface ISavegameAdapter
{

}
