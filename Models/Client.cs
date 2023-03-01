namespace Models
{
    public class Client : Person
    {
        public Client() { }
        public Client(string _passportID, string _name)
        {
            PassportID = _passportID;
            Name = _name;
        }
        public Client(string _passportID, DateTime _dateTime, string _name)
        {
            PassportID = _passportID;
            DateOfBirth = _dateTime;
            Name = _name;
        }
        public override int GetHashCode()
        {
            return (PassportID.GetHashCode() + DateOfBirth.GetHashCode() + Name.GetHashCode());
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Client client) return (PassportID == client.PassportID);
            return false;
        }
    }
}
