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

        // Called when the web page boots up.
        // Selects a project from the made with unity showcase.
        // Displays all available content related to the project.
        public void OnGet()
        {
            string selectedSite = SelectSite();

            HtmlNode masterNode = ParseSelectedSite(selectedSite);

            MakePage(masterNode);
        }

        // Selecting a site is a 3 step process:
        // First, we must parse UserData.json, which contains a mapping from website on the made with unity 
        //      page (string) to whether that site has been visited within the current round (1 if yes, 0 if no)
        // Second, we must selects a site from the set of unvisited sites
        // Third, we must update UserData.json to properly reflect that we have chosen a new site
        // Returns the newly selected site
        private string SelectSite() {
            // First, parse Json file holding information about visited and unvisited sites
            string dataFile = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Data").Where(file => {
                return file.Substring(file.Length - 5) == ".json";
            }).ToArray()[0];
            string json = System.IO.File.ReadAllText(dataFile);
            JsonArray jsonDoc = (JsonArray)JsonObject.Parse(json);

            // Our map holding all mappings and our list of unvisited sites
            Dictionary<string, int> sites = new Dictionary<string, int>();
            List<string> unvisitedSites = new List<string>();

            // Fill our map and list with the data
            foreach (JsonObject obj in jsonDoc)
            {
                // Derive the values from the json Object
                JsonValue name, visited;
                obj.TryGetValue("Key", out name);
                obj.TryGetValue("Value", out visited);

                // Store the values
                sites[name.ToString()] = int.Parse(visited.ToString());
                if (visited.ToString() == "0")
                    unvisitedSites.Add(name.ToString());
            }

            // If our list of unvisited sites is empty then all sites have been visited.
            if (unvisitedSites.Count == 0)
            {
                // We've seen them all! Reset all values in the map to unseen for the next round
                List<string> keys = new List<string>(sites.Keys);
                foreach (string key in keys)
                    sites[key] = 0;
                unvisitedSites = keys;
            } 
            // Select our next site
            int selectedIndex = (new Random()).Next(0, unvisitedSites.Count);
            string selectedSite = unvisitedSites[selectedIndex];
            sites[selectedSite] = 1;
            selectedSite = selectedSite.Replace("\"", "");
            
            // Update data file with the newly selected site
            string jsonResult = JsonConvert.SerializeObject(sites.ToArray());
            jsonResult = jsonResult.Replace("\\\"", "");
            using (StreamWriter writer = new StreamWriter(dataFile, false)){ 
                writer.Write(jsonResult);
            }

            return selectedSite;
        }

        // Given the randomly chosen website, this will parse its html
        // and return the a node that is the root of the page's main body 
        private HtmlNode ParseSelectedSite(string selectedSite) {
            HttpClient client = new HttpClient();
            var response = Task.Run(() => {
                return client.GetAsync(selectedSite);
            }).Result;

            var pageContents = Task.Run(() => {
                return response.Content.ReadAsStringAsync();
            }).Result;
            
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            return pageDocument.DocumentNode.SelectSingleNode("//div[@class='block-region-content']");
        }

        // Given the node that is the root of the page's main body
        // this extracts all text, images, videos, and links.
        // The extracted information is stored for our cshtml page to display.
        private void MakePage(HtmlNode masterNode) {
            // Extract title information
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
                Images[i,0] = ExtractSrc(imageNodes[i].OuterHtml);
                Images[i,1] = ExtractField(imageNodes[i].OuterHtml, "alt");
                Images[i,2] = ExtractField(imageNodes[i].OuterHtml, "title");
                Images[i,3] = ExtractField(imageNodes[i].OuterHtml, "class");
                // If this image is a thumbnail for a video, mark it as such
                if (Images[i,3] == "yt-thumb embed-responsive-item") {
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
        private string ExtractSrc(string tag) {
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

        // Given a string and the index after a quotation, 
        // this returns the remainder of that string before the next quotation mark
        private string ExtractFromQuotations(string str, int startIndex) {
            int endIndex = startIndex;
            while (str.ElementAt(endIndex) != '\"') endIndex++;
            return str.Remove(endIndex).Substring(startIndex);
        }
    }
}
