using System;
using System.Collections.Generic;
using System.Net.Http;
using Common;
using Newtonsoft.Json;

namespace ConsoleSearch
{
    public class App
    {
        HttpClient _client = new HttpClient();
        public App()
        {
        }

        public void Run()
        {   

            Console.WriteLine("Console Search");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine();
                if (input.Equals("q")) break;

                var query = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var response = _client.GetAsync("http://localhost:5001/search/" + String.Join(",",query) + "/10");
                //var result = mSearchLogic.Search(query, 10);

                var resultStr = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<SearchResult>(resultStr);

                if (result.Ignored.Count > 0) {
                    Console.WriteLine("Ignored: ");
                    foreach (var aWord in result.Ignored)
                    {
                        Console.WriteLine(aWord + ", ");
                    }
                }

                int idx = 0;
                foreach (var doc in result.DocumentHits) {
                    Console.WriteLine("" + (idx+1) + ": " + doc.Document.mUrl + " -- contains " + doc.NoOfHits + " search terms");
                    Console.WriteLine("Index time: " + doc.Document.mIdxTime + ". Creation time: " + doc.Document.mCreationTime);
                    Console.WriteLine("Missing: " + ArrayAsString(doc.Missing.ToArray()));
                    idx++;
                }
                Console.WriteLine("Documents: " + result.Hits + ". Time: " + result.TimeUsed.TotalMilliseconds);
            }
        }

//         public async Task RunAsync()
// {
//     Console.WriteLine("Console Search");

//     while (true)
//     {
//         Console.WriteLine("enter search terms - q for quit");
//         string input = Console.ReadLine();
//         if (input.Equals("q")) break;

//         var query = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

//         try
//         {
//             var response = await _client.GetAsync("http://localhost:5001/search/" + String.Join(",",query) + "/10");

//             var resultStr = await response.Content.ReadAsStringAsync();
//             var result = JsonConvert.DeserializeObject<SearchResult>(resultStr);

//             if (result.Ignored.Count > 0)
//             {
//                 Console.WriteLine("Ignored: ");
//                 foreach (var aWord in result.Ignored)
//                 {
//                     Console.WriteLine(aWord + ", ");
//                 }
//             }

//             int idx = 0;
//             foreach (var doc in result.DocumentHits)
//             {
//                 Console.WriteLine("" + (idx + 1) + ": " + doc.Document.mUrl + " -- contains " + doc.NoOfHits + " search terms");
//                 Console.WriteLine("Index time: " + doc.Document.mIdxTime + ". Creation time: " + doc.Document.mCreationTime);
//                 Console.WriteLine("Missing: " + ArrayAsString(doc.Missing.ToArray()));
//                 idx++;
//             }
//             Console.WriteLine("Documents: " + result.Hits + ". Time: " + result.TimeUsed.TotalMilliseconds);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"An error occurred while searching: {ex.Message}");
//         }
//     }
// }

        string ArrayAsString(string[] s) {
            if (s.Length == 0)
                return "[]";
            string res = "[";
            foreach (var str in s)
                res += str + ", ";
            return res.Substring(0, res.Length - 2) + "]";
        }
    }
}
