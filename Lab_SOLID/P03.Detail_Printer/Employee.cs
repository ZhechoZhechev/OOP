namespace P03.DetailPrinter
{
using P03.Detail_Printer;
    public class Employee : IEmployee
    {
        public Employee(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public virtual string Print()
        {
            return this.Name;
        }
    }
}
