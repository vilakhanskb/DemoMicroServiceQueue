using System.ComponentModel.DataAnnotations;

namespace Touyang.Entities
{
    public class CustomerDb
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; } = new();
    }

    public class Datum
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
    }


}
