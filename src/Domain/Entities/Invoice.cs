namespace Domain.Entities
{
    public class Invoice
    {

        private readonly Company _company;

        public Company Company
        {
            get { return _company; }
        }
        private readonly Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
        }

        public decimal Ammout { get; set; }

        public decimal TotalAmountWithholdTaxes
        {
            get
            { return IRAmmoutWithhold + PISAmmoutWithhold + COFINSAmmoutWithhold + CSLLAmmoutWithhold; }
        }

        public decimal IRAmmoutWithhold { get; set; }
        public decimal PISAmmoutWithhold { get; set; }
        public decimal COFINSAmmoutWithhold { get; set; }
        public decimal CSLLAmmoutWithhold { get; set; }

        public Invoice(Company company, Customer customer)
        {
            _company = company;
            _customer = customer;
        }


    }
}