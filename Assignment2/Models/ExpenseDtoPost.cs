using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Assignment2.Models
{
    public class ExpenseDtoPost
    {
        [Microsoft.Build.Framework.Required]
        public long ModelId { get; set; }
        [Microsoft.Build.Framework.Required]
        public long JobId { get; set; }
        [Microsoft.Build.Framework.Required]
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("text")]
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        [Microsoft.Build.Framework.Required]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
    }
}
