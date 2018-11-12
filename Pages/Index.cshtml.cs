using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Json;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using HtmlAgilityPack;

namespace MadeWithUnityShowCase.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } // TODO: remove
        public string Title { get; set; }
        public string Studio { get; set; }
        public string TitleImage { get; set; }
        public string Text { get; set;}
        public string[,] Images { get; set; }
        public string[,] Videos { get; set; }
        public string[,] Links { get; set; }

        public void OnGet()
        {
            // Parse Json file holding information about seen and unseen sites
            string dataFile = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Data").Where(file => {
                return file.Substring(file.Length - 5) == ".json";
            }).ToArray()[0];
            string json = System.IO.File.ReadAllText(dataFile);
            JsonArray jsonDoc = (JsonArray)JsonObject.Parse(json);

            // Initialize variables for parsing the json object
            Dictionary<string, int> sites = new Dictionary<string, int>();
            List<string> unvisitedSites = new List<string>();

            foreach (JsonObject obj in jsonDoc)
            {
                // Derive the values from the json Object
                JsonValue name, visited;
                obj.TryGetValue("Key", out name);
                obj.TryGetValue("Value", out visited);
                Message += name.ToString() + " </br>" + visited.ToString();

                // Store the values
                sites[name.ToString()] = int.Parse(visited.ToString());
                if (visited.ToString() == "0")
                    unvisitedSites.Add(name.ToString());
            }

            // Check if all sites have been visited
            if (unvisitedSites.Count == 0)
            {
                // We've seen them all! Reset all values in the array to 0 to start over again
                List<string> keys = new List<string>(sites.Keys);
                foreach (string key in keys)
                    sites[key] = 0;

                unvisitedSites = keys;
            } 
            int selectedIndex = (new Random()).Next(0, unvisitedSites.Count);
            string selectedSite = unvisitedSites[selectedIndex].Replace("\"", "");
            sites[selectedSite] = 1;
            
            // Update Data file with the newly selected site
            string jsonResult = JsonConvert.SerializeObject(sites.ToArray());
            jsonResult = jsonResult.Replace("\\\"", "");
            using (StreamWriter writer = new StreamWriter(dataFile, false)){ 
                writer.Write(jsonResult);
            }
            Message = jsonResult + " " + selectedSite;
            // Display properties of selected site
            //ParseSite(selectedSite).ConfigureAwait(true).GetAwaiter().GetResult();
            HttpClient client = new HttpClient();
            var response = Task.Run(() => {
                return client.GetAsync(selectedSite);
            }).Result;

            var pageContents = Task.Run(() => {
                return response.Content.ReadAsStringAsync();
            }).Result;
            
            Message = selectedSite;
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            HtmlNode masterNode = pageDocument.DocumentNode.SelectSingleNode("//div[@class='block-region-content']");

            // Set up title header
            Title = masterNode.SelectSingleNode("(//h1)").InnerText;
            Studio = masterNode.SelectSingleNode("(//div[contains(@class,'section-hero-studio')])").InnerText;
            var headerNode = masterNode.SelectSingleNode("div[@class='section section-story-hero']");
            TitleImage = ExtractBackgroundImage(headerNode.OuterHtml);

            // Extract all Text
            var currTextNode = headerNode.NextSibling;
            while (currTextNode.NextSibling != null) {
                Text += currTextNode.InnerText;
                currTextNode = currTextNode.NextSibling;
            }

            // Extract all pictures
            HtmlNodeCollection imageNodes = masterNode.SelectNodes("//img");
            Images = new string[imageNodes.Count - 2, 4];
            for (int i = 0; i < imageNodes.Count - 2; i++) {
                Images[i,0] = ExtractImageSrc(imageNodes[i].OuterHtml);
                Images[i,1] = ExtractImageAlt(imageNodes[i].OuterHtml);
                Images[i,2] = ExtractImageTitle(imageNodes[i].OuterHtml);
                Images[i,3] = ExtractImageClass(imageNodes[i].OuterHtml);
                if (Images[i,3] == "yt-thumb embed-responsive-item") {
                    // Mark as video
                }
            }
        }

        // Extracts just the style for a background image of a divider
        private string ExtractBackgroundImage(string divTag) {
            return divTag.Substring(divTag.IndexOf("https"))
                .Remove(divTag.IndexOf("\">"))
                .Replace(" ", "")
                .Replace("&#039;", "");
        }

        // Extracts just the source of any image
        // If no source, returns empty string
        private string ExtractImageSrc(string imageTag) {
            if (!imageTag.Contains("src"))
                return "";
            int startPos= imageTag.IndexOf("src=\"") + 5,
                endPos = startPos;
            while (imageTag.ElementAt(endPos) != '\"') endPos++;
            return "https://unity.com" + imageTag.Remove(endPos).Substring(startPos);
        }

        // Extracts just the alt of any image tag
        // If no alt, returns empty string
        private string ExtractImageAlt(string imageTag) {
            if (!imageTag.Contains("alt"))
                return "";
            int startPos= imageTag.IndexOf("alt=\"") + 5,
                endPos = startPos;
            while (imageTag.ElementAt(endPos) != '\"') endPos++;
            return imageTag.Remove(endPos).Substring(startPos);
        }

        // Extracts just the title of any image tag
        // If no title, returns empty string
        private string ExtractImageTitle(string imageTag) {
            if (!imageTag.Contains("title"))
                return "";
            int startPos= imageTag.IndexOf("title=\"") + 7,
                endPos = startPos;
            while (imageTag.ElementAt(endPos) != '\"') endPos++;
            return imageTag.Remove(endPos).Substring(startPos);
        }

        // Extracts just the class of any image tag
        // If no class, returns empty string
        private string ExtractImageClass(string imageTag) {
            if (!imageTag.Contains("class"))
                return "";
            int startPos= imageTag.IndexOf("class=\"") + 7,
                endPos = startPos;
            while (imageTag.ElementAt(endPos) != '\"') endPos++;
            return imageTag.Remove(endPos).Substring(startPos);
        }
    }
}
