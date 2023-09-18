using CsvHelper.Configuration.Attributes;

namespace CSVReaderNet.Models
{
    public class Resum
    {
        [Index(0)]
        public string Name { get; set; }
        [Index(1)]
        public DateOnly Date { get; set; }
        [Index(2)]
        public bool Maried { get; set; }
        [Index(3)]
        public string Phone { get; set; }
        [Index(4)]
        public decimal Salary { get; set; }
    }
}
