namespace DbModels.Dtos;

    public class ClientDto
    {
        public string PassportID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Bonus { get; set; }
        public List<AccountDB> Accounts { get; set; }
    }