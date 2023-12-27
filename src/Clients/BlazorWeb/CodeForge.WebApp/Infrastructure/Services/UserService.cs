using System.Net.Http.Json;
using System.Text.Json;
using CodeForge.Common.Infrastructure.Exceptions;
using CodeForge.Common.Infrastructure.Responses;
using CodeForge.Common.ViewModels.Queries;
using CodeForge.Common.ViewModels.RequestModels;
using CodeForge.WebApp.Infrastructure.Services.Interfaces;

namespace CodeForge.WebApp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
    {
        var userDetail = await _client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");

        return userDetail;
    }

    public async Task<UserDetailViewModel> GetUserDetail(string userName)
    {
        var userDetail = await _client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");

        return userDetail;
    }

    public async Task<bool> UpdateUser(UserDetailViewModel user)
    {
        var res = await _client.PostAsJsonAsync($"/api/user/update", user);

        return res.IsSuccessStatusCode;
    }

    public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
    {
        var command = new UserChangePasswordCommand(oldPassword, newPassword, null);
        var httpResponse = await _client.PostAsJsonAsync($"/api/User/password/change", command);

        if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
        {
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var responseStr = await httpResponse.Content.ReadAsStringAsync();
                var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                responseStr = validation.FlattenErrors;
                throw new DbValidationException(responseStr);
            }

            return false;
        }

        return httpResponse.IsSuccessStatusCode;
    }
}
