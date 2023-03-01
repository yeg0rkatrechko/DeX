using Models;
using Newtonsoft.Json;

namespace Services
{
    public class CurrencyService
    {

        public async Task<CurrencyToConvertResponse> ConvertCurrency(CurrencyToConvert data)
        {
            CurrencyToConvertResponse response;

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync($"https://www.amdoren.com/api/currency.php?api_key={data.Key}" +
                                                                            $"&from={data.InputCurrency}&to={data.OutputCurrency}&amount={data.Amount}");

                string message = await responseMessage.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<CurrencyToConvertResponse>(message);

            }

            return response;
        }
    }
}
