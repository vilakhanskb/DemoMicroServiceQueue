
using System.ComponentModel.DataAnnotations;

namespace VilakhanBlazor.Entities
{
    public class CustomerDb
    {
        public CustomerDb()
        {
            data = new();
        }
        public int code { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
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
