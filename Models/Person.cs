namespace Models
{
    public abstract class Person
    {
        public Guid ID { get; set; }
        public string PassportID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}