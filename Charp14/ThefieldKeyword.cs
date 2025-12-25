using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Charp14
{
    internal class ThefieldKeyword
    {
        void Main()
        {
            var p = new ThefieldKeywordPerson();
            p.Name = "  Alice  ";     // Automatically trimmed
            Console.WriteLine(p.Name); // "Alice"

            p.Name = null;

            var db = new DatabaseConnection();
            db.QueryLog.Add("SELECT * FROM Users");  // field is initialized here
            Console.WriteLine(db.QueryLog.Count);   // 1

            var s = new Settings();
            Console.WriteLine(s.MaxRetries); // 3

            s.MaxRetries = 10;
            Console.WriteLine(s.MaxRetries); // 10

            s.MaxRetries = -5;
            Console.WriteLine(s.MaxRetries); // 0 (clamped)

            var c = new Customer();
            c.PhoneNumber = "  123-456-7890  ";
            Console.WriteLine(c.PhoneNumber); // "123-456-7890"

            c.PhoneNumber = "   ";
            Console.WriteLine(c.PhoneNumber is null); // True

            var config = new Config { ConnectionString = "Server=prod" }; // OK
        }


    }

    //public class ThefieldKeyword
    //{
    //    private string _name = string.Empty; // Old way – manual field

    //    public string Name
    //    {
    //        get => _name;
    //        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    //    }
    //}

    // C# 14 – clean auto-property with validation
    public class ThefieldKeywordPerson
    {
        public string Name
        {
            get => field;
            set => field = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
        }
    }

    public class DatabaseConnection
    {
        public List<string> QueryLog
        {
            get => field ??= new List<string>();  // Lazy init on first read
                                                  // No set needed if read-only to consumers
        }
    }

    //public class ViewModel : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler? PropertyChanged;

    //    private string _title = "Default";
    //    public string Title
    //    {
    //        get => _title;
    //        set
    //        {
    //            if (_title != value)
    //            {
    //                _title = value;
    //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
    //            }
    //        }
    //    }
    //}

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Title
        {
            get => field;
            set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }
    }

    public class Settings
    {
        public int MaxRetries
        {
            get => field;
            set => field = value < 0 ? 0 : value;  // Ensure non-negative
        } = 3;  // Default value
    }

    public class Customer
    {
        public string? PhoneNumber
        {
            get => field;
            set => field = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }

    public class Config
    {
        public string ConnectionString
        {
            init => field = value ?? throw new ArgumentNullException(nameof(value));
        } = "DefaultConnection";
    }

    public class Order
    {
        public decimal? Discount
        {
            get => field;
            set => field = value > 1m ? 1m : (value < 0m ? 0m : value); // Clamp 0-1
        }

        public decimal Total { get; set; } = 100m;

        public decimal FinalPrice => Total * (1 - (Discount ?? 0m));
    }
}
