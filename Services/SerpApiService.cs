using Newtonsoft.Json.Linq;
using SerpApi;
using System.Collections;
using WebSearchMCPServer.Model;

namespace WebSearchMCPServer.Services;

public class SerpApiService
{
    private readonly IConfiguration _configuration;
    private GoogleSearch _googleSearch;

    public SerpApiService(IConfiguration configuration)
    {
        _configuration = configuration;
        _googleSearch= new GoogleSearch(_configuration.GetRequiredSection("SerpApiSettings")["ApiKey"]);
    }

    public List<OrganicResult> Search(string searchQuery, string location)
    {
        Hashtable ht = GetGoogleParameterContext();
        ht.Add("q", searchQuery);
        if (!string.IsNullOrEmpty(location)) ht.Add("location", location);

        try
        {
            _googleSearch.parameterContext = ht;
            
            JObject data = _googleSearch.GetJson();
            JArray results = (JArray)data["organic_results"];

            return results.ToObject<List<OrganicResult>>() ?? new List<OrganicResult>();
        }
        catch (SerpApiSearchException ex)
        {
            // TODO>>
            throw;
        }
    }

    private Hashtable GetGoogleParameterContext()
    {
        IConfigurationSection googleParameters = _configuration.GetRequiredSection("SerpApiSettings")
            .GetRequiredSection("GoogleSearchEngineApiResultsParameters");

        Hashtable ht = new Hashtable();
        ht.Add("hl", googleParameters["hl"]);
        ht.Add("gl", googleParameters["gl"]);
        ht.Add("google_domain", googleParameters["google_domain"]);

        return ht;
    }
}
