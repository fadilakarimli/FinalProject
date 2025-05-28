using FinalProjectConsume.Models.Tour;
using FinalProjectConsume.Services.Interfaces;
using System.Net.Http.Headers;

namespace FinalProjectConsume.Services
{
    public class TourService : ITourService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7145/api/admin/Tour/";

        public TourService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<HttpResponseMessage> CreateAsync(TourCreate model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Name), "Name");
            content.Add(new StringContent(model.Duration), "Duration");

            content.Add(new StringContent(model.CountryIds.Count.ToString()), "CountryCount");

            content.Add(new StringContent(model.Price.ToString()), "Price");
            if (model.OldPrice != null)
                content.Add(new StringContent(model.OldPrice.ToString()), "OldPrice");

            content.Add(new StringContent(model.CityId.ToString()), "CityId");

            foreach (var activityId in model.ActivityIds)
                content.Add(new StringContent(activityId.ToString()), "ActivityIds");

            foreach (var amenityId in model.AmenityIds)
                content.Add(new StringContent(amenityId.ToString()), "AmenityIds");

            foreach (var countryId in model.CountryIds) 
                content.Add(new StringContent(countryId.ToString()), "CountryIds");

            if (model.ImageFile != null)
            {
                var stream = model.ImageFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", model.ImageFile.FileName);
            }

            return await _httpClient.PostAsync($"{_baseUrl}Create", content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"{_baseUrl}Delete/{id}");
        }

        public async Task<HttpResponseMessage> EditAsync(int id, TourEdit model)
        {
            using var content = new MultipartFormDataContent();

            content.Add(new StringContent(model.Name), "Name");
            content.Add(new StringContent(model.Duration), "Duration");
            content.Add(new StringContent(model.CountryCount.ToString()), "CountryCount");
            content.Add(new StringContent(model.Price.ToString()), "Price");
            if (model.OldPrice != null)
                content.Add(new StringContent(model.OldPrice.ToString()), "OldPrice");
            content.Add(new StringContent(model.CityId.ToString()), "CityId");

            if (model.ImageFile != null)
            {
                var stream = model.ImageFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", model.ImageFile.FileName);
            }

            if (!string.IsNullOrEmpty(model.ExistingImageUrl))
            {
                content.Add(new StringContent(model.ExistingImageUrl), "ExistingImageUrl");
            }

            return await _httpClient.PutAsync($"{_baseUrl}Edit/{id}", content);
        }

        public async Task<IEnumerable<Tour>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Tour>>($"{_baseUrl}GetAll");
        }

        public async Task<Tour> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}GetById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var tour = await response.Content.ReadFromJsonAsync<Tour>();

            return tour;
        }


    }
}
