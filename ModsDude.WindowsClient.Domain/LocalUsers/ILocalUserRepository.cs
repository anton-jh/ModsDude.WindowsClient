namespace ModsDude.WindowsClient.Domain.LocalUsers;
public interface ILocalUserRepository
{
    Task<IEnumerable<LocalUser>> GetAllAsync();
    Task SaveAsync(LocalUser user);
}
