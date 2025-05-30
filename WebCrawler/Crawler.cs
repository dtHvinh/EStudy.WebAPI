using HtmlAgilityPack;

namespace WebCrawler;

public class Crawler
{
    private static readonly LinkPreview _empty = new()
    {
        Title = "No title found",
        Description = "No description found",
        Image = "No image found"
    };

    public class LinkPreview
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Image { get; set; } = default!;
    }

    public static async Task<LinkPreview> Get(string url, CancellationToken cancellationToken = default)
    {
        if (!ValidateUrl(url))
            return _empty;

        using var httpClient = new HttpClient();
        string html = await httpClient.GetStringAsync(url, cancellationToken);

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        string? title = htmlDoc.DocumentNode.SelectSingleNode("//title")?.InnerText?.Trim();

        HtmlNodeCollection metaTags = htmlDoc.DocumentNode.SelectNodes("//meta");
        string? description = null;
        string? image = null;

        if (metaTags != null)
        {
            foreach (HtmlNode tag in metaTags)
            {
                var nameAttr = tag.GetAttributeValue("name", "");
                var propAttr = tag.GetAttributeValue("property", "");
                var content = tag.GetAttributeValue("content", "");

                if (nameAttr == "description" || propAttr == "og:description")
                    description ??= content;

                if (propAttr == "og:image")
                    image ??= content;
            }
        }

        return new LinkPreview
        {
            Title = title ?? "No title found",
            Description = description ?? "No description found",
            Image = image ?? "No image found"
        };
    }

    private static bool ValidateUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

