using ScalableTeams.HumanResourcesManagement.Application.Interfaces;
using ScalableTeams.HumanResourcesManagement.Domain.VacationRequests.Entities;

namespace ScalableTeams.HumanResourcesManagement.Infrastucture.Services;

public class AccountingService : IAccountingService
{
    private readonly IHttpClientFactory httpClientFactory;

    public AccountingService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task NotifyVacationsRequest(VacationRequest vacationRequest, CancellationToken cancellationToken)
    {
        var requestedUrl = "/http/200/Ok";

        using HttpClient httpClient = httpClientFactory.CreateClient(nameof(AccountingService));

        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestedUrl);

        using HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception("Server returned an invalid response.");
        }
    }
}
