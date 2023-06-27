//using Access.Sql.Entities;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Access.Sql.Configurations
//{
//    public class ProcedureToothConfiguration : BaseEntityTypeConfiguration<ProcedureTooth>
//    {
//        public override void Configure(EntityTypeBuilder<ProcedureTooth> builder)
//        {
//            base.Configure(builder);

//            builder
//                .HasOne(x => x.Procedure)
//                .WithMany(x => x.ProcedureTeeth)
//                .HasForeignKey(x => x.ProcedureId);

//            builder
//                .HasOne(x => x.Tooth)
//                .WithMany(x => x.ProcedureTeeth)
//                .HasForeignKey(x => x.ToothId);

//            builder.HasIndex(x => new { x.ToothId, x.ProcedureId });
//        }
//    }
//}
