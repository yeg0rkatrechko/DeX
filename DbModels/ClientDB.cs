using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DbModels
{
    [Table(name: "Clients")]
    public class ClientDB
    {
            [Key]
            [Column(name: "ID_Client")]
            public Guid Id { get; set; }

            [Column(name: "Passport_ID")]
            public string PassportID { get; set; }

            [Column(name: "Name")]
            public string Name { get; set; }

            [Column(name: "Birth_Date")]
            public DateTime DateOfBirth { get; set; }            

            [Column(name: "Bonus")]
            public int Bonus { get; set; }

            [Column(name: "Accounts_ID")]
            public List<AccountDB> Accounts { get; set; }
    }
}