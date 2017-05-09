namespace Domain.Entities
{
    public class Tax
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Rate
        {
            get; set;
        }

        public Tax(string name, string description, decimal rate)
        {
            this.Name = name;
            this.Description = description;
            this.Rate = rate;
        }

        public virtual decimal Apply(decimal value)
        {
            return value * (this.Rate/100);
        }

    }

}