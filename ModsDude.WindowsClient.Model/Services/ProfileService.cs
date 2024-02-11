using ModsDude.WindowsClient.ApiClient.Generated;
using ModsDude.WindowsClient.Model.Exceptions;

namespace ModsDude.WindowsClient.Model.Services;
public class ProfileService(
    IProfileClient profileClient)
{
    public delegate void ProfileListChangedEventHandler(Guid? profileIdOfInterest);
    public event ProfileListChangedEventHandler? ProfileListChanged;


    public async Task<IEnumerable<ProfileDto>> GetProfiles(Guid repoId, CancellationToken cancellationToken)
    {
        return await profileClient.GetAllAsync(repoId, cancellationToken);
    }

    public async Task CreateProfile(Guid repoId, string name, CancellationToken cancellationToken)
    {
        var request = new CreateProfileRequest()
        {
            Name = name
        };

        ProfileDto profile;

        try
        {
            profile = await profileClient.CreateProfileAsync(repoId, request, cancellationToken);
        }
        catch (ApiException ex) when (ex.StatusCode == 409)
        {
            throw new UserFriendlyException("Name taken", null, ex);
        }
        OnProfileListChanged(profile.Id);
    }

    public async Task UpdateProfile(Guid repoId, Guid profileId, string name, CancellationToken cancellationToken)
    {
        var request = new UpdateProfileRequest()
        {
            Name = name
        };

        try
        {
            await profileClient.UpdateAsync(repoId, profileId, request, cancellationToken);
        }
        catch (ApiException ex) when (ex.StatusCode == 409)
        {
            throw new UserFriendlyException("Name taken", null, ex);
        }
        OnProfileListChanged(profileId);
    }

    public async Task DeleteProfile(Guid repoId, Guid profileId, CancellationToken cancellationToken)
    {
        await profileClient.DeleteAsync(repoId, profileId, cancellationToken);

        OnProfileListChanged(null);
    }

    private void OnProfileListChanged(Guid? idOfInterest)
    {
        ProfileListChanged?.Invoke(idOfInterest);
    }
}
