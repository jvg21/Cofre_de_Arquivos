namespace FileSageAPI.Entities.Url
{
    public static class UrlFunctions
    {
        public static string GenerateUrlName(string urlContent)
        {
            if (urlContent.Contains("stackoverflow.com")) return "Stack Overflow";
            if (urlContent.Contains("instagram.com")) return "Instagram";
            if (urlContent.Contains("facebook.com")) return "Facebook";
            if (urlContent.Contains("github.com")) return "GitHub";
            if (urlContent.Contains("tiktok.com")) return "TikTok";
            if (urlContent.Contains("wattpad.com")) return "Wattpad";
            if (urlContent.Contains("wikipedia.org")) return "Wikipedia";
            if (urlContent.Contains("wiktionary.org")) return "Wiktionary";
            if (urlContent.Contains("youtube.com") || urlContent.Contains("youtu.be")) return "YouTube";
            if (urlContent.Contains("gitlab.com")) return "GitLab";
            if (urlContent.Contains("https://archive.org")) return "Internet Archive";
            if (urlContent.Contains("reddit.com")) return "Reddit";
            if (urlContent.Contains("twitter.com") || urlContent.Contains("x.com")) return "X";
            if (urlContent.Contains("archiveofourown.org")) return "AO3";
            if (urlContent.Contains("medium.com")) return "Medium";
            if (urlContent.Contains("fanfiction.net")) return "FFN";
            if (urlContent.Contains("imdb.com")) return "IMDb";
            if (urlContent.Contains("amazon.com")) return "Amazon";
            if (urlContent.Contains("goodreads.com")) return "Goodreads";

            return urlContent;
        }
    }
}
