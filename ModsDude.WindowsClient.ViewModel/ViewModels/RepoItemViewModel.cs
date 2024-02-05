using CommunityToolkit.Mvvm.ComponentModel;
using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public partial class RepoItemViewModel(
    CombinedRepo combinedRepo)
    : ObservableObject
{
    [ObservableProperty]
    private string _name = combinedRepo.Name;


    internal CombinedRepo Repo { get; } = combinedRepo;
}
