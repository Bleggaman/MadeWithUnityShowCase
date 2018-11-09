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

namespace MadeWithUnityShowCase.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            //Message = HttpRuntime.AppDomainAppPath; //CookieAuthenticationDefaults.CookiePrefix;
            

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
                Message += name.ToString() + " " + visited.ToString();

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
            
            // Update Data file with the newly selected site
            string jsonResult = JsonConvert.SerializeObject(sites.ToArray());
            jsonResult = jsonResult.Replace("\\\"", "");
            using (StreamWriter writer = new StreamWriter(dataFile, false)){ 
                writer.Write(jsonResult);
            }
            Message = jsonResult + " " + selectedSite;
            // Display properties of selected site
            //Message = selectedSite;
        }

    /*
        private void SaveProject(string fileName)
        {
            bool originalRenamed = false;
            string tempNewFile = null;
            string oldFileTempName = null;
            Exception exception = null;

            try
            {
                tempNewFile = GetTempFileName(Path.GetDirectoryName(fileName));

                using (Stream tempNewFileStream = File.Open(tempNewFile, FileMode.CreateNew))
                {
                    SafeXmlSerializer xmlFormatter = new SafeXmlSerializer(typeof(Project));
                    xmlFormatter.Serialize(tempNewFileStream, Project);
                }

                if (File.Exists(fileName))
                {
                    oldFileTempName = GetTempFileName(Path.GetDirectoryName(fileName));
                    File.Move(fileName, oldFileTempName);
                    originalRenamed = true;
                }

                File.Move(tempNewFile, fileName);
                originalRenamed = false;

                CurrentProjectPath = fileName;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                if (tempNewFile != null) TryToDelete(tempNewFile);

                if (originalRenamed)
                {
                    try
                    {
                        File.Move(oldFileTempName, fileName);
                        originalRenamed = false;
                    }
                    catch { }
                }

                if (exception != null) MessageBox.Show(exception.Message);

                if (originalRenamed)
                {
                    MessageBox.Show("'" + fileName + "'" +
                        " have been corrupted or deleted in this operation.\n" +
                        "A backup copy have been created at '" + oldFileTempName + "'");
                }
                else if (oldFileTempName != null) TryToDelete(oldFileTempName);
            }
        } */
    }
}
