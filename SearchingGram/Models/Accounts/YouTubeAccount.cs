using Newtonsoft.Json;
using SearchingGram.Models.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Models.Accounts
{
    public class YouTubeAccount : Account, IYouTubeResponse
    {
        public string ChanelId { get; set; }

        public string Subscribers { get; set; }

        public string Views { get; set; }
        
        public string _viewsList { get; set; }
        
        public string _likes { get; set; }
        
        public string _dislikes { get; set; }
        
        public long _mostLiked { get; set; }
        
        public long _mostDisliked { get; set; }

        
        
        public string _commentsCounts { get; set; }

        public string VideosCount { get; set; }

        public string _videoNames { get; set; }
       
        public string _viewsGrows { get; set; }
        //Property to access data from _viewsGrowsfield.
        // return dictionary of date and number 
        // to store data in Db I serialize Dictinary as Json and converting it to string 
        // on get string get deserialized into Dictionary
        [NotMapped]
        public Dictionary<string, ulong?> GrowViews
        {
            get { return JsonConvert.DeserializeObject < Dictionary < string , ulong?>>
                    (string.IsNullOrEmpty(_viewsGrows) ? "{}" : _viewsGrows); }
            set
            {
                _viewsGrows = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
        }
        //Property to access data from _viewsList field.
        // return list of numbers 
        // to store data in Db I convert Db to string
        // on get string get deserialized into list
        [NotMapped]
        public List<ulong> ViewsList
        {
            get {
                if (_viewsList == null)
                {
                    return new List<ulong> { 0 };
                }
                    return _viewsList.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => ulong.Parse(x)).ToList(); }
            set
            {
                _viewsList= string.Join(";", value);
            }
        }
        [NotMapped]
        public List<ulong> Likes
        {
            get { if (_likes == null) { return new List<ulong> { 0 }; } 
                return _likes.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => ulong.Parse(x)).ToList(); }
            set
            {
                _likes = string.Join(";", value);
            }
        }
        [NotMapped]
        public List<ulong> Dislikes
        {
            get { if (_dislikes == null) { return new List<ulong> { 0 }; } 
                return _dislikes.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => ulong.Parse(x)).ToList(); }
            set
            {
                _dislikes = string.Join(";", value);
            }
        }
        [NotMapped]
        public List<ulong> CommentsCounts
        {
            get { if (_commentsCounts == null) { return new List<ulong> { 0 }; } 
                return _commentsCounts.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => ulong.Parse(x)).ToList(); }
            set
            {
                _commentsCounts = string.Join(";", value);
            }
        }
        [NotMapped]
        public List<string> VideoNames
        {
            get { if (_videoNames == null) { return new List<string>() { "" }; } return _videoNames.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => x).ToList(); }
            set
            {
                _videoNames = string.Join(";", value);
            }
        }
        [NotMapped]
        public ulong? MostLiked
        {
            get
            {
                return (ulong?)_mostLiked;
            }
            set
            {
                _mostLiked = (long)value;
            }
        }
        [NotMapped]
        public ulong? MostDisliked
        {
            get
            {
                return (ulong?)_mostDisliked;
            }
            set
            {
                _mostDisliked = (long)value;
            }
        }




    }
}
