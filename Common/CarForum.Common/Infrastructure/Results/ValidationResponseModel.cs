using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarForum.Common.Infrastructure.Results
{
    public  class ValidationResponseModel
    {
        public IEnumerable<string> Errors { get; set; }

        public ValidationResponseModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }
        public ValidationResponseModel(string message):this(new List<string>() { message })
        {

        }
        [JsonIgnore]
        public string FlatternErrors => Errors!= null
                ? string.Join(Environment.NewLine,Errors)
                :string.Empty;
    }
}
