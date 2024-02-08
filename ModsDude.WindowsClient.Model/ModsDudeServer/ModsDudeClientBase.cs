using ModsDude.WindowsClient.Model.Services;

namespace ModsDude.WindowsClient.Model.ModsDudeServer;
public abstract class ModsDudeClientBase(
    SessionOld session)
{
    protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
        var msg = new HttpRequestMessage();
        msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", session.AccessToken);
        return Task.FromResult(msg);
    }
}
