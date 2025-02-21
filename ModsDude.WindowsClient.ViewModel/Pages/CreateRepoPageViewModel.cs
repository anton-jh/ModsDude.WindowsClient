using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModsDude.WindowsClient.Model.GameAdapters;
using ModsDude.WindowsClient.Model.Services;
using System.Collections.ObjectModel;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class CreateRepoPageViewModel(
    RepoService repoService)
    : PageViewModel
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _name = "New repo";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private GameAdapterDescriptor? _selectedGameAdapter;


    public bool IsValid =>
        !string.IsNullOrEmpty(Name) &&
        SelectedGameAdapter is not null;

    public ObservableCollection<GameAdapterDescriptor> AvailableGameAdapters { get; } =
    [
        new(new("fs", "1"), "Farming Simulator", [], "Description longo"),
        new(new("beam", "1"), "BeamNG.Drive - BeamMP", [], "Description longo dos")
    ];


    [RelayCommand(CanExecute = nameof(IsValid))]
    private async Task Submit(CancellationToken cancellationToken)
    {
        if (SelectedGameAdapter is null)
        {
            return;
        }

        await repoService.CreateRepo(
            Name,
            SelectedGameAdapter.Value.Id.ToString(),
            "",
            cancellationToken);
    }
}
