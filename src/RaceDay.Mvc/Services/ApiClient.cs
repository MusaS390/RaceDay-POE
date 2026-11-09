using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace RaceDay.Mvc.Services;
public class ApiClient(HttpClient http, IConfiguration config, IHttpContextAccessor accessor)
{
    static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);
    HttpContext Ctx => accessor.HttpContext!;
    void Prepare()
    {
        http.BaseAddress ??= new Uri(config["Api:BaseUrl"] ?? "http://localhost:5000/");
        http.DefaultRequestHeaders.Remove("Cookie");
        var cookie=Ctx.Session.GetString("ApiCookie");
        if (!string.IsNullOrWhiteSpace(cookie)) http.DefaultRequestHeaders.Add("Cookie",cookie);
    }
    async Task<HttpResponseMessage> Send(HttpRequestMessage req)
    {
        Prepare();
        var response=await http.SendAsync(req);
        if(response.Headers.TryGetValues("Set-Cookie",out var values))
        {
            var cookie=values.Select(x=>x.Split(';')[0]).FirstOrDefault(x=>x.StartsWith(".RaceDay.Session="));
            if(cookie!=null) Ctx.Session.SetString("ApiCookie",cookie);
        }
        return response;
    }
    public async Task<(bool Ok,string? Error,JsonElement Data)> Post(string path, object body)
    {
        using var req=new HttpRequestMessage(HttpMethod.Post,path){Content=new StringContent(JsonSerializer.Serialize(body,Json),Encoding.UTF8,"application/json")};
        using var r=await Send(req); var raw=await r.Content.ReadAsStringAsync();
        return (r.IsSuccessStatusCode,r.IsSuccessStatusCode?null:ExtractError(raw),Parse(raw));
    }
    public async Task<(bool Ok,string? Error,JsonElement Data)> Put(string path, object body)
    {
        using var req=new HttpRequestMessage(HttpMethod.Put,path){Content=new StringContent(JsonSerializer.Serialize(body,Json),Encoding.UTF8,"application/json")};
        using var r=await Send(req); var raw=await r.Content.ReadAsStringAsync();
        return (r.IsSuccessStatusCode,r.IsSuccessStatusCode?null:ExtractError(raw),Parse(raw));
    }
    public async Task<(bool Ok,string? Error,JsonElement Data)> Get(string path)
    {
        using var req=new HttpRequestMessage(HttpMethod.Get,path); using var r=await Send(req); var raw=await r.Content.ReadAsStringAsync();
        return (r.IsSuccessStatusCode,r.IsSuccessStatusCode?null:ExtractError(raw),Parse(raw));
    }
    public async Task<(bool Ok,string? Error,JsonElement Data)> Delete(string path)
    {
        using var req=new HttpRequestMessage(HttpMethod.Delete,path); using var r=await Send(req); var raw=await r.Content.ReadAsStringAsync();
        return (r.IsSuccessStatusCode,r.IsSuccessStatusCode?null:ExtractError(raw),Parse(raw));
    }
    public async Task<(bool Ok,string? Error)> Upload(string path, IFormFile file)
    {
        using var form=new MultipartFormDataContent(); await using var stream=file.OpenReadStream();
        using var content=new StreamContent(stream); content.Headers.ContentType=MediaTypeHeaderValue.Parse(file.ContentType);
        form.Add(content,"file",Path.GetFileName(file.FileName));
        using var req=new HttpRequestMessage(HttpMethod.Post,path){Content=form}; using var r=await Send(req);
        return (r.IsSuccessStatusCode,r.IsSuccessStatusCode?null:ExtractError(await r.Content.ReadAsStringAsync()));
    }
    static JsonElement Parse(string s) { try{return JsonDocument.Parse(string.IsNullOrWhiteSpace(s)?"{}":s).RootElement.Clone();}catch{return JsonDocument.Parse("{}").RootElement.Clone();} }
    static string ExtractError(string s) { try{return JsonDocument.Parse(s).RootElement.TryGetProperty("message",out var m)?m.GetString()??"The API request failed.":"The API request failed. Check that the Part 2 API is running.";}catch{return "The API request failed. Check that the Part 2 API is running.";} }
}
