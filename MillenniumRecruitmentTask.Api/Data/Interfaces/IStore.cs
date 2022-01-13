using MillenniumRecruitmentTask.Api.Data.Entities;
using System.Collections.Generic;

namespace MillenniumRecruitmentTask.Api.Data.Interfaces
{
    public interface IStore
    {
        public List<User> Users { get; set; }
    }
}
