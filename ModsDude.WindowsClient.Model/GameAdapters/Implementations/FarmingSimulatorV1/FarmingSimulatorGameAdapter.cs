namespace ModsDude.WindowsClient.Model.GameAdapters.Implementations.FarmingSimulatorV1;
public class FarmingSimulatorGameAdapter : IGameAdapter
{
    public GameAdapterDescriptor Descriptor { get; } = new(
        Id: new("_farming_simulator", "1"),
        DisplayName: "Farming Simulator",
        CompatibleWithGames: ["Farming Simulator 25"],
        Description: "For Farming Simulator 25.");
}
// TODO: List adapters when creating a repo
// TODO: Separate mods- and savegame-adapters?
