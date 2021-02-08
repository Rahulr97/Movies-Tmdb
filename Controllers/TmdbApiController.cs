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

namespace piquota.Controllers
{
   
    public class TmdbApiController : Controller
    {
        
        public void CallAPI(string searchText, int PageNo)
        {
            string url = "https://api.themoviedb.org/3/search/movie";
            string apikey = "107c1d9499aab019dd11605773c06ac2";
            HttpWebRequest getRequest = WebRequest.Create(url + "?api_key=" + apikey + "&language=en-US&query=" + searchText) as HttpWebRequest;
            string apiResponse = "";
            
            using (HttpWebResponse response = getRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            ResponseSearchMovie rootObject = JsonConvert.DeserializeObject<ResponseSearchMovie>(apiResponse);

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"resultDiv\"><p>Names</p>");
            foreach (Result result in rootObject.results)
            {
                string image = result.profile_path == null ? Url.Content("~/Content/Image/no-image.png") : "https://image.tmdb.org/t/p/w500/" + result.profile_path;
                string link = Url.Action("GetMovie", "TmdbApi", new { id = result.id });

                sb.Append("<div class=\"result\" resourceId=\"" + result.id + "\">" + "<a href=\"" + link + "\"><img src=\"" + image + "\" />" + "<p>" + result.name + "</a></p></div>");
            }

            ViewBag.Result = "Test";
            ViewData["Result"] = "Test";
            
            int pageSize = 20;
            PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.CurrentPage = PageNo;
            pagingInfo.TotalItems = rootObject.total_results;
            pagingInfo.ItemsPerPage = pageSize;
            ViewBag.Paging = pagingInfo;
        }


        public ActionResult Index(string Movie, int PageNo)
        {
            //CallAPI(Movie, PageNo);
            //Models.Movie theMovieDb = new Models.Movie();
            //theMovieDb.searchText = Movie;
            ViewBag.Result1 = "Test1";
            ViewData["Result1"] = "Test1";
            return View();
        }
        //[HttpPost]
        //public ActionResult Index(Models.Movie theMovieDb, string searchText)
        //{

        //    //if (ModelState.IsValid)
        //    //{
        //    //    CallAPI(searchText,0);
        //    //}
        //    //return View(theMovieDb);
        //}


        public ActionResult GetMovie(int id)
        {
            /*Calling API https://developers.themoviedb.org/3/movie */
            string apiKey = "3356865d41894a2fa9bfa84b2b5f59bb";
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            /*End*/

            /*http://json2csharp.com*/
            ResponseMovie rootObject = JsonConvert.DeserializeObject<ResponseMovie>(apiResponse);
            Movie theMovieDb = new Movie();
            theMovieDb.title = rootObject.title;
            theMovieDb.overview = rootObject.overview;
            theMovieDb.production_companies = rootObject.production_companies;
            theMovieDb.release_date = rootObject.release_date;
            theMovieDb.imdb_id = rootObject.imdb_id;
            theMovieDb.runtime = rootObject.runtime;
            theMovieDb.production_companies = rootObject.production_companies;
            

            return View(theMovieDb);
        }
    }

}