﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModsDude.WindowsClient.Model.Models;
using ModsDude.WindowsClient.Model.Services;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class RepoAdminPageViewModel(
    RepoService repoService,
    RepoModel repo)
        : PageViewModel
{
    [ObservableProperty]
    private string _name = repo.Name;

    public bool UseModsFeature { get; } = repo.ModsScript is not null;
    public bool UseSavegamesFeature { get; } = repo.SavegamesScript is not null;
    public string? ModsScript { get; } = repo.ModsScript;
    public string? SavegamesScript { get; } = repo.SavegamesScript;


    [RelayCommand]
    private async Task SaveChanges(CancellationToken cancellationToken)
    {
        await repoService.UpdateRepo(repo.Id, Name, cancellationToken);

    }

    [RelayCommand]
    private Task DeleteRepo(CancellationToken cancellationToken)
    {
        return repoService.DeleteRepo(repo.Id, cancellationToken);
    }
}
