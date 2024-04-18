using FoodShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodShop.Data.Configurations
{
    public class TradeMarksEntityConfiguration : IEntityTypeConfiguration<TradeMark>
    {
        public void Configure(EntityTypeBuilder<TradeMark> builder)
        {
            builder.HasData(this.GenerateTradeMarks());
        }

        private TradeMark[] GenerateTradeMarks()
        {
            ICollection<TradeMark> tradeMarks = new HashSet<TradeMark>();

            TradeMark tradeMark;

            tradeMark = new TradeMark()
            {
                Id = 1,
                Name = "Vereya"
            };
            tradeMarks.Add(tradeMark);

            tradeMark = new TradeMark()
            {
                Id = 2,
                Name = "Sayana"
            };
            tradeMarks.Add(tradeMark);

            tradeMark = new TradeMark()
            {
                Id = 3,
                Name = "Mio"
            };
            tradeMarks.Add(tradeMark);

            tradeMark = new TradeMark()
            {
                Id = 4,
                Name = "Simid"
            };
            tradeMarks.Add(tradeMark);

            tradeMark = new TradeMark()
            {
                Id = 5,
                Name = "Horisont"
            };
            tradeMarks.Add(tradeMark);

            tradeMark = new TradeMark()
            {
                Id = 6,
                Name = "Bagryanka"
            };
            tradeMarks.Add(tradeMark);

            return tradeMarks.ToArray();
        }
    }
}
