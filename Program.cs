using System.Text.Json;
using ConsumerViaCep.Models;
using static System.Console;

Write("Digite o seu CEP: ");
string? cep = ReadLine();

string url = $"https://viacep.com.br/ws/{cep}/json/";

WriteLine($"Consultando: {url}");

using HttpClient client = new HttpClient();

try
{
    HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    string json = await response.Content.ReadAsStringAsync();
    Endereco? endereco = JsonSerializer.Deserialize<Endereco>(json);

    if (endereco != null)
    {
        WriteLine();
        WriteLine($"CEP: {endereco.Cep}");
        WriteLine($"Rua: {endereco.Logradouro}");
        WriteLine($"Bairro: {endereco.Bairro}");
        WriteLine($"Cidade: {endereco.Localidade}");
        WriteLine($"UF: {endereco.Uf}");
    }
}
catch (Exception e)
{
    WriteLine("Aconteceu um erro ao consultar a API: " + e.Message);
}
