using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModels
{
    [Table(name: "Accounts")]
    public class AccountDB
    {
        [Key]
        [Column(name: "Account_ID")]
        public Guid Id { get; set; }

        [Column(name: "Amount")]
        public decimal Amount { get; set; }

        [Column(name: "Currency")]
        public CurrencyDB Currency { get; set; }

        [Column(name: "Client")]
        public ClientDB Client { get; set; }

        [Column(name: "Client_id")]
        public Guid ClientId { get; set; }
    }
}
