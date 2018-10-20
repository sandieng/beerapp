using System.Collections.Generic;

namespace BeerRosterAPI.ViewModels
{
    public class ResponseVM<T> where T: class
    {
        public List<T> Payload { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public ErrorVM Error { get; set; }
    }
}
