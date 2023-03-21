namespace Models
{
    public class Client : Person
    {
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
