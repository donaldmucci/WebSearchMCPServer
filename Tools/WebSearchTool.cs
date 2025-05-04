using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using WebSearchMCPServer.Model;
using WebSearchMCPServer.Services;

namespace WebSearchMCPServer.Tools;

[McpServerToolType]
public sealed class WebSearchTool
{
    private readonly SerpApiService _serpApiService;

    public WebSearchTool(SerpApiService serpApiService)
    {
        this._serpApiService = serpApiService;
    }

    [McpServerTool, Description("Executes a web search on the internet.")]
    public async Task<string> Search(string searchQuery)
    {
        List<OrganicResult> organicResults = _serpApiService.Search(searchQuery, null);

        string rtn = string.Empty;
        for (int i = 0; i < organicResults.Count; i++)
            rtn += $"{organicResults[i].Title} ({organicResults[i].Link}):\r\n{organicResults[i].Snippet}\r\n\n";

        return rtn;
    }
}
