using SearchingGram.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Controllers.Responses
{
    public class AccountsInfoResponse
    {
        public List<IInstaResponse> Instagram { get; set; }

        public List<IYouTubeResponse> YouTube { get; set; }

        public List<ITwitterResponse> Twitter { get; set; }
    }
}
