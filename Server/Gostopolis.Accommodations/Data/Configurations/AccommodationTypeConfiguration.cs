namespace Gostopolis.Accommodations.Data.Configurations;

using Gostopolis.Accommodations.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AccommodationTypeConfiguration : IEntityTypeConfiguration<AccommodationType>
{
    public void Configure(EntityTypeBuilder<AccommodationType> builder)
    {
        builder
            .HasKey(o => o.Id);

        builder
            .HasData(
                new AccommodationType(1, "Хотел", "Място за настаняване, често предлагащо ресторанти, конферентни зали и други удобства за посетителите."),
                new AccommodationType(2, "Къща за гости", "Частна собственост с отделни помещения за гостите и собственика."),
                new AccommodationType(3, "Пансион със закуска (BB)", "Частен дом, предлагащ нощувка и закуска."),
                new AccommodationType(4, "Частна квартира", "Споделен дом, в който гостите имат собствена стая, а домакинът живее в останалите помещения. Някои удобства са споделени между домакините и гостите."),
                new AccommodationType(5, "Хостел", "Място за настаняване за гости с ограничен бюджет, с леглова база предимно от типа туристическа спалня и приятелска атмосфера."),
                new AccommodationType(6, "Апартхотел", "Апартамент с място за самостоятелно приготвяне на храна и някои хотелски удобства, например рецепция."),
                new AccommodationType(7, "Хотел капсула (Capsule hotel)", "Типичен японски хотел, съдържащ множество крайно малки стаи (капсули), създадени да осигурят единствено нощувка и не предлагащи услугите, които повечето хотели предлагат."),
                new AccommodationType(8, "Селска къща", "Частен дом с прости условия за настаняване извън града."),
                new AccommodationType(9, "Хан (Inn)", "Малко място за настаняване, с основни удобства и рустикален дух."),
                new AccommodationType(10, "Мотел", "Крайпътен хотел с директен достъп до паркинг и ограничени удобства."),
                new AccommodationType(11, "Хотелски комплекс", "Място за почивка с ресторанти, различни възможности за отдих, често с усещане за лукс."),
                new AccommodationType(12, "Зелен хотел (Green hotel)", "Хотел, чието функциониране се подчинява на екологични и природосъобразни принципи, като например пестене на вода, електроенергия, намаляване на отпадъците и пр."),
                new AccommodationType(13, "Риад (Riad)", "Традиционно мароканско място за настаняване с двор и усещане за лукс."),
                new AccommodationType(14, "Риокан (Ryokan)", "Традиционно японско място за настаняване с възможности за хранене."),
                new AccommodationType(15, "Хижа", "Частно жилище сред природата, например в планината или гората."));
    }
}