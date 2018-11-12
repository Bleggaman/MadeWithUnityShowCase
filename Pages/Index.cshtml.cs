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
        // For every Image (first level) this holds the source, alt, title, and class (respectively) on the second level
        public string[,] Images { get; set; }
        // For every Video (first level) this holds an id, a class, a data tag, a image overlay index,
        //      and video text (respectively) on the second level
        public string[,] Videos { get; set; }
        // For every Link (first level) this holds the source and inner text (respectively) on the second level
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
            string selectedSite = unvisitedSites[selectedIndex];
            sites[selectedSite] = 1;
            selectedSite = selectedSite.Replace("\"", "");
            
            // Update Data file with the newly selected site
            string jsonResult = JsonConvert.SerializeObject(sites.ToArray());
            jsonResult = jsonResult.Replace("\\\"", "");
            using (StreamWriter writer = new StreamWriter(dataFile, false)){ 
                writer.Write(jsonResult);
            }
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

            // Extract all pictures & mark any videos
            List<int> videoIndeces = new List<int>();
            HtmlNodeCollection imageNodes = masterNode.SelectNodes("//img");
            Images = new string[imageNodes.Count - 2, 4];
            for (int i = 0; i < imageNodes.Count - 2; i++) {
                Images[i,0] = ExtractImageSrc(imageNodes[i].OuterHtml);
                Images[i,1] = ExtractField(imageNodes[i].OuterHtml, "alt");
                Images[i,2] = ExtractField(imageNodes[i].OuterHtml, "title");
                Images[i,3] = ExtractField(imageNodes[i].OuterHtml, "class");
                if (Images[i,3] == "yt-thumb embed-responsive-item") {
                    // Mark as video
                    videoIndeces.Add(i);
                }
            }

            // Set up all videos
            Videos = new string[videoIndeces.Count, 5];
            for (int i = 0; i < videoIndeces.Count; i++) {
                int imageIndex = videoIndeces[i];
                var videoNode = imageNodes[imageIndex].ParentNode;
                string updatedHTML = videoNode.OuterHtml.Substring(videoNode.OuterHtml.IndexOf("id="));
                Videos[i, 1] = ExtractField(updatedHTML, "class");
                string dataField = "data-" + Videos[i, 1].Substring(0, 2);
                Videos[i, 2] = ExtractField(updatedHTML, dataField);
                Videos[i, 3] = Images[imageIndex,0];
                Videos[i, 4] = videoNode.InnerText;
            }

            // Extract all Links
            HtmlNodeCollection linkNodes = masterNode.SelectNodes("//a");
            Links = new string[linkNodes.Count, 2];
            for (int i = 0; i < linkNodes.Count; i++) {
                Links[i, 0] = ExtractField(linkNodes[i].OuterHtml, "href");
                Links[i, 1] = linkNodes[i].InnerText;
            }
        }

        /////////////
        // Extractors

        // Extracts just the style for a background image of a divider
        private string ExtractBackgroundImage(string divTag) {
            return divTag.Substring(divTag.IndexOf("https"))
                .Remove(divTag.IndexOf("\">"))
                .Replace(" ", "")
                .Replace("&#039;", "");
        }

        // Extracts just the source of any image
        // If no source, returns empty string
        private string ExtractImageSrc(string tag) {
            if (!tag.Contains("src"))
                return "";
            return "https://unity.com" + ExtractFromQuotations(tag, tag.IndexOf("src=\"") + 5);
        }

        // Extracts just the href of any tag
        // If no href, returns empty string
        private string ExtractField(string tag, string field) {
            if (!tag.Contains(field))
                return "";
            return ExtractFromQuotations(tag, tag.IndexOf(field + "=\"") + field.Count() + 2);
        }

        private string ExtractFromQuotations(string str, int startIndex) {
            int endIndex = startIndex;
            while (str.ElementAt(endIndex) != '\"') endIndex++;
            return str.Remove(endIndex).Substring(startIndex);
        }
    }
}
