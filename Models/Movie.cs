using System;
using System.Collections.Generic;
using System.Web;

namespace piquota.Models
{
    public class Movie
    {   
            public string searchText { get; set; }
            public bool adult { get; set; }
            public string id { get; set; }
            public string imdb_id { get; set; }
            public string title { get; set; }
            public string overview { get; set; }
            public string poster_path { get; set; }
            public List<ProductionCompany> production_companies { get; set; }
            public List<genre> genres { get; set; }
            public string homepage { get; set; }
            public string budget { get; set; }
            public string release_date { get; set; }
            public string revenue { get; set; }
            public double vote_average { get; set; }
            public double popularity { get; set; }
            public int runtime { get; set; }
       
    }
    public class Result
    {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string budget { get; set; }
        public double popularity { get; set; }
    }
    public class ResponseSearchMovie
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }

    public class ProductionCompany
    {
        public int id { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class genre
    {

        public int id { get; set; }
        public string g { get; set; }

        public string name { get; set; }
    }


    public class ResponseMovie
    {
        public bool adult { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public string budget { get; set; }
        public string original_language { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<genre> genres { get; set; }
        public string poster_path { get; set; }
        public string status { get; set; }
        public string release_date { get; set; }
        public string revenue { get; set; }
        public string imdb_id { get; set; }
        public string name { get; set; }
        public string vote_average { get; set; }
        public double popularity { get; set; }
        public int runtime { get; set; }
    }
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems /
                    ItemsPerPage);
            }
        }
    }
}
