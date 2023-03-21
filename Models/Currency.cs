using System.ComponentModel;

namespace Models
{
    public class Currency : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public int Code { get; set; }


        public Currency() { }
        public Currency(string _name, int _code)
        {
            Code = _code;
            Name = _name;
        }


    }
}
