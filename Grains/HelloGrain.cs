using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans.Runtime;
using Orleans.Serialization.Invocation;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Grains;

public class DeserializerDTOMicMacPaddyWhack
{
    public Dictionary<string, string> headers;
    public string origin;
    public string url; 
}

public class HelloGrain : Grain, IHello
{
    private readonly ILogger _logger;

    public HelloGrain(ILogger<HelloGrain> logger) => _logger = logger;

    ValueTask<string> IHello.SayHello(string greeting)
    {
        _logger.LogInformation("""
            SayHello message received: greeting = "{Greeting}"
            """,
            greeting);

        return ValueTask.FromResult($"""

            Client said: "{greeting}", so HelloGrain says: Hello!
            """);
    }
    ValueTask<int> IHello.Multiply(int a, int b)
    {
        var result = a * b;
        _logger.LogInformation("""
            Multiply message received: "{a} * {b} = {result}"
            """,
            a, b, result);

        return ValueTask.FromResult(result);
    }
    ValueTask<int> IHello.AModB(int a, int b)
    {
        var result = a % b;
        _logger.LogInformation("""
            Multiply message received: "{a} % {b} = {result}"
            """,
            a, b, result);

        return ValueTask.FromResult(result);
    }

    async Task<string> IHello.Pulldata()
    {
        string uri = "http://httpbin.org/get";

        using HttpClient _client = new HttpClient();
        using HttpResponseMessage response = await _client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        
        string result = response.Content.ReadAsStringAsync().Result; 
        var StatusCode = response.StatusCode;
        if(StatusCode != HttpStatusCode.OK)
        {
            return "error";
        }

        DeserializerDTOMicMacPaddyWhack obj = JsonConvert.DeserializeObject<DeserializerDTOMicMacPaddyWhack>(result);
        Console.WriteLine($"""
            talking to {obj.origin} succeeded
            Host: {obj.headers["Host"]} 
            """);

        return "200";
    }

    async Task<string> IHello.PullHttpsData()
    {
        string uri = "https://httpbin.org/get";
        ///System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        HttpClient httpClient = new HttpClient();

        //specify to use TLS 1.2 as default connection
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        httpClient.BaseAddress = new Uri(uri);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

        using HttpResponseMessage response = await httpClient.GetAsync(uri);

        response.EnsureSuccessStatusCode();

        string result = response.Content.ReadAsStringAsync().Result;
        var StatusCode = response.StatusCode;
        if (StatusCode != HttpStatusCode.OK)
        {
            return "error";
        }

        DeserializerDTOMicMacPaddyWhack obj = JsonConvert.DeserializeObject<DeserializerDTOMicMacPaddyWhack>(result);
        Console.WriteLine($"""
            talking to {obj.origin} succeeded
            Host: {obj.headers["Host"]} 
            """);

        return "200";
    }

    async Task<string> IHello.PullXboxInventoryData()
    {
        string uri = "https://httpbin.org/";
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        using HttpClient _client = new HttpClient();
        //_client.DefaultRequestHeaders.Accept.Add(AuthorizationHeader.Authorization,);
        //_client.GetAsync().;
        using HttpResponseMessage response = await _client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        string result = response.Content.ReadAsStringAsync().Result;
        var StatusCode = response.StatusCode;
        if (StatusCode != HttpStatusCode.OK)
        {
            return "error";
        }

        DeserializerDTOMicMacPaddyWhack obj = JsonConvert.DeserializeObject<DeserializerDTOMicMacPaddyWhack>(result);
        Console.WriteLine($"""
            talking to {obj.origin} succeeded
            Host: {obj.headers["Host"]} 
            """);

        return "200";
    }
}