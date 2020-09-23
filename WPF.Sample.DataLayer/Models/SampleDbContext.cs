using Common.Library;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace WPF.Sample.DataLayer
{
    public partial class SampleDbContext : DbContext
    {
        public SampleDbContext() : base("name=Samples")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFeedback> UserFeedback { get; set; }



        // 09/23/2020 10:14 am - SSN - [20200923-0823] - [002] - M07-03 - Demo: Entity Framework classes and validation 

        public List<ValidationMessage> CreateValidationMessages(DbEntityValidationException ex)
        {
            List<ValidationMessage> ret = new List<ValidationMessage>();

            foreach (DbValidationError error in ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors))
            {
                ret.Add(new ValidationMessage
                {
                    Message = error.ErrorMessage,
                    PropertyName = error.PropertyName
                });

            }

            return ret;
        }


    }


}