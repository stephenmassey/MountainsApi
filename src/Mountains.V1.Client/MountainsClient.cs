using Mountains.V1.Client.Dtos;
using System;
using System.Linq;
using System.Net;
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

        public async Task<MountainCollectionDto> GetMountainsAsync(int? start = null, int? count = null)
        {
            MountainCollectionDto mountainCollection = null;
            HttpResponseMessage response = await _client.GetAsync(BuildQuery("mountains", new { start, count }));
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

        public async Task<MountainRangeCollectionDto> GetMountainRangesAsync(int? start = null, int? count = null)
        {
            MountainRangeCollectionDto mountainRangeCollection = null;
            HttpResponseMessage response = await _client.GetAsync(BuildQuery("mountainranges", new { start, count }));
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

        public async Task<HikeCollectionDto> GetHikesAsync(int? start = null, int? count = null, string mountainId = null, string userId = null)
        {
            HikeCollectionDto hikeCollection = null;
            HttpResponseMessage response = await _client.GetAsync(BuildQuery("hikes", new { start, count, mountainId, userId }));
            if (response.IsSuccessStatusCode)
                hikeCollection = await response.Content.ReadAsAsync<HikeCollectionDto>();

            return hikeCollection;
        }

        public async Task<HikeDto> GetHikeAsync(string hikeId)
        {
            HikeDto hike = null;
            HttpResponseMessage response = await _client.GetAsync($"hikes/{hikeId}");
            if (response.IsSuccessStatusCode)
                hike = await response.Content.ReadAsAsync<HikeDto>();

            return hike;
        }

        public async Task<HikeDto> CreateHikeAsync(HikeDto hike)
        {
            HikeDto newHike = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("hikes", hike);
            if (response.IsSuccessStatusCode)
                newHike = await response.Content.ReadAsAsync<HikeDto>();

            return newHike;
        }

        public async Task<bool> DeleteHikeAsync(string hikeId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"hikes/{hikeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<UserCollectionDto> GetUsersAsync(int? start = null, int? count = null)
        {
            UserCollectionDto userCollection = null;
            HttpResponseMessage response = await _client.GetAsync(BuildQuery("users", new { start, count }));
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

        public async Task<UserDto> SignInAsync(UserDto user)
        {
            UserDto newUser = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("users/signin", user);
            if (response.IsSuccessStatusCode)
                newUser = await response.Content.ReadAsAsync<UserDto>();

            return newUser;
        }

        public async Task<bool> SignOutAsync()
        {
            HttpResponseMessage response = await _client.DeleteAsync("users/signout");
            return response.IsSuccessStatusCode;
        }

        private string BuildQuery(string requestUri, object parameters)
        {
            if (parameters == null)
                return requestUri;

            var queryParameters = parameters.GetType().GetProperties()
                .Select(x => Tuple.Create(x.Name, x.GetValue(parameters)))
                .Where(x => x.Item2 != null)
                .Select(x => x.Item1 + "=" + WebUtility.UrlEncode(x.Item2.ToString()));

            return queryParameters.Any() ? requestUri + "?" + queryParameters.Aggregate((i, j) => i + "&" + j) : requestUri;
        }

        private HttpClient _client;
    }
}
