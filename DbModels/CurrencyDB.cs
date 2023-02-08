using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModels
{
    [Table(name: "Currencies")]
    public class CurrencyDB
    {
        [Key]
        [Column(name: "Currency_ID")]
        public Guid Id { get; set; }

        [Column(name: "Name")]
        public string Name { get; set; }

        [Column(name: "Code")]
        public int Code { get; set; }

        [Column(name: "Accounts_ID")]
        public List<AccountDB> Accounts { get; set; }
    }
}
