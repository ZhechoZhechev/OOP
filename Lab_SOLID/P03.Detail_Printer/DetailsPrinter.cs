namespace P03.DetailPrinter
{
    using System.Collections.Generic;
    using P03.Detail_Printer;
    public class DetailsPrinter
    {
        private IList<IEmployee> employees;

        public DetailsPrinter(IList<IEmployee> employees)
        {
            this.employees = employees;
        }

        public void PrintDetails()
        {
            foreach (Employee employee in this.employees)
            {
                employee.Print();
            }
        }


    }
}
