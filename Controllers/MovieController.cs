using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using piquota.Models;
using System.Text;
using RestSharp;
namespace piquota.Controllers
{
   
    public class MovieController : Controller
    {
        
        public void CallAPI(string searchText, int PageNo)
        {
            string url = "https://api.themoviedb.org/3/search/movie";
            string apikey = "107c1d9499aab019dd11605773c06ac2";
            string requesturl = url + "?api_key=" + apikey + "&language=en-US&query=" + searchText;
            

            var client = new RestClient(requesturl);
            client.Timeout = -1;
            RestRequest request = new RestRequest();
            request.Method = Method.GET; 
            IRestResponse response = client.Execute(request);
            Console.WriteLine("Test");
            Console.WriteLine(response.Content);

           

            ResponseSearchMovie rootObject = JsonConvert.DeserializeObject<ResponseSearchMovie>(response.Content);

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"resultDiv\"><p>Names</p>");
            foreach (Result result in rootObject.results)
            {
                string image = result.poster_path == null ? Url.Content("~/Content/Image/no-image.png") : "https://image.tmdb.org/t/p/w500/" + result.poster_path;
                string link = Url.Action("GetMovie", "Movie", new { id = result.id });

                sb.Append("<div class=\"result\" resourceId=\"" + result.id + "\">" + "<a href=\"" + link + "\"><img src=\"" + image + "\" />" + "<p>" + result.name + "</a></p></div>");
            }

            ViewBag.Result = sb.ToString();

            
            int pageSize = 20;
            PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.CurrentPage = PageNo;
            pagingInfo.TotalItems = rootObject.total_results;
            pagingInfo.ItemsPerPage = pageSize;
            ViewBag.Paging = pagingInfo;
        }


        public ActionResult Index(string Movie, int PageNo)
        {
            if (Movie == null || Movie == "")
            {
                return View();
            }
            else
            {
                CallAPI(Movie, PageNo);
                Models.Movie theMovieDb = new Models.Movie();
                theMovieDb.searchText = Movie;
                return View(theMovieDb);
            }
            
        }
        [HttpPost]
        public ActionResult Index(Models.Movie theMovieDb, string searchText)
        {

            if (ModelState.IsValid)
            {
                CallAPI(searchText, 0);
            }
            return View(theMovieDb);
        }


        public ActionResult GetMovie(int id)
        {
            /*Calling API https://developers.themoviedb.org/3/movie */
            string apiKey = "3356865d41894a2fa9bfa84b2b5f59bb";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;
            string requesturl = "https://api.themoviedb.org/3/movie/" + id + "?api_key=" + apiKey + "&language=en-US";

            var client = new RestClient(requesturl);
            client.Timeout = -1;
            RestRequest request = new RestRequest();
            request.Method = Method.GET; 
            IRestResponse response = client.Execute(request);
            Console.WriteLine("Test");
            Console.WriteLine(response.Content);


            ResponseMovie rootObject = JsonConvert.DeserializeObject<ResponseMovie>(response.Content);
            Movie theMovieDb = new Movie();
            theMovieDb.poster_path = rootObject.poster_path == null ? Url.Content("~/Content/Image/no-image.png") : "https://image.tmdb.org/t/p/w500/" + rootObject.poster_path;
            theMovieDb.title = rootObject.title;
            theMovieDb.overview = rootObject.overview;
            theMovieDb.popularity = rootObject.popularity;
            theMovieDb.release_date = rootObject.release_date;
            theMovieDb.imdb_id = rootObject.imdb_id;
            theMovieDb.runtime = rootObject.runtime;
            
            

            return View(theMovieDb);
        }
    }

}