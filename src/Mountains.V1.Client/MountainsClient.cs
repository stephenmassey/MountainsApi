using Mountains.V1.Client.Dtos;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mountains.V1.Client
{
    public sealed class MountainsClient
    {
        public MountainsClient(Uri baseUri)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUri;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<MountainCollectionDto> GetMountainsAsync()
        {
            MountainCollectionDto mountainCollection = null;
            HttpResponseMessage response = await _client.GetAsync("mountains");
            if (response.IsSuccessStatusCode)
                mountainCollection = await response.Content.ReadAsAsync<MountainCollectionDto>();

            return mountainCollection;
        }

        public async Task<MountainDto> GetMountainAsync(string mountainId)
        {
            MountainDto mountain = null;
            HttpResponseMessage response = await _client.GetAsync($"mountains/{mountainId}");
            if (response.IsSuccessStatusCode)
                mountain = await response.Content.ReadAsAsync<MountainDto>();

            return mountain;
        }

        public async Task<MountainDto> CreateMountainAsync(MountainDto mountain)
        {
            MountainDto newMountain = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("mountains", mountain);
            if (response.IsSuccessStatusCode)
                newMountain = await response.Content.ReadAsAsync<MountainDto>();

            return newMountain;
        }

        public async Task<MountainDto> UpdateMountainAsync(string mountainId, MountainDto mountain)
        {
            MountainDto newMountain = null;
            HttpResponseMessage response = await _client.PutAsJsonAsync($"mountains/{mountainId}", mountain);
            if (response.IsSuccessStatusCode)
                newMountain = await response.Content.ReadAsAsync<MountainDto>();

            return newMountain;
        }

        public async Task<bool> DeleteMountainAsync(string mountainId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"mountains/{mountainId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<MountainRangeCollectionDto> GetMountainRangesAsync()
        {
            MountainRangeCollectionDto mountainRangeCollection = null;
            HttpResponseMessage response = await _client.GetAsync("mountainranges");
            if (response.IsSuccessStatusCode)
                mountainRangeCollection = await response.Content.ReadAsAsync<MountainRangeCollectionDto>();

            return mountainRangeCollection;
        }

        public async Task<MountainRangeDto> GetMountainRangeAsync(string mountainRangeId)
        {
            MountainRangeDto mountainRange = null;
            HttpResponseMessage response = await _client.GetAsync($"mountainranges/{mountainRangeId}");
            if (response.IsSuccessStatusCode)
                mountainRange = await response.Content.ReadAsAsync<MountainRangeDto>();

            return mountainRange;
        }

        public async Task<MountainCollectionDto> GetMountainsInMountainRangeAsync(string mountainRangeId)
        {
            MountainCollectionDto mountainCollection = null;
            HttpResponseMessage response = await _client.GetAsync($"mountainranges/{mountainRangeId}/mountains");
            if (response.IsSuccessStatusCode)
                mountainCollection = await response.Content.ReadAsAsync<MountainCollectionDto>();

            return mountainCollection;
        }

        public async Task<MountainRangeDto> CreateMountainRangeAsync(MountainRangeDto mountainRange)
        {
            MountainRangeDto newMountainRange = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("mountainranges", mountainRange);
            if (response.IsSuccessStatusCode)
                newMountainRange = await response.Content.ReadAsAsync<MountainRangeDto>();

            return newMountainRange;
        }

        public async Task<MountainRangeDto> UpdateMountainRangeAsync(string mountainRangeId, MountainRangeDto mountainRange)
        {
            MountainRangeDto newMountainRange = null;
            HttpResponseMessage response = await _client.PutAsJsonAsync($"mountainranges/{mountainRangeId}", mountainRange);
            if (response.IsSuccessStatusCode)
                newMountainRange = await response.Content.ReadAsAsync<MountainRangeDto>();

            return newMountainRange;
        }

        public async Task<bool> DeleteMountainRangeAsync(string mountainRangeId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"mountainranges/{mountainRangeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<UserCollectionDto> GetUsersAsync()
        {
            UserCollectionDto userCollection = null;
            HttpResponseMessage response = await _client.GetAsync("users");
            if (response.IsSuccessStatusCode)
                userCollection = await response.Content.ReadAsAsync<UserCollectionDto>();

            return userCollection;
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            UserDto user = null;
            HttpResponseMessage response = await _client.GetAsync($"users/{userId}");
            if (response.IsSuccessStatusCode)
                user = await response.Content.ReadAsAsync<UserDto>();

            return user;
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            UserDto newUser = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("users", user);
            if (response.IsSuccessStatusCode)
                newUser = await response.Content.ReadAsAsync<UserDto>();

            return newUser;
        }

        public async Task<UserDto> UpdateUserAsync(string userId, UserDto user)
        {
            UserDto newUser = null;
            HttpResponseMessage response = await _client.PutAsJsonAsync($"users/{userId}", user);
            if (response.IsSuccessStatusCode)
                newUser = await response.Content.ReadAsAsync<UserDto>();

            return newUser;
        }

        private HttpClient _client;
    }
}
