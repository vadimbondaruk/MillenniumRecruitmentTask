using MillenniumRecruitmentTask.Api.Data.Entities;
using MillenniumRecruitmentTask.Api.Data.Interfaces;
using System.Collections.Generic;

namespace MillenniumRecruitmentTask.Api.Data
{
    public class Store : IStore
    {
        public List<User> Users { get; set; } = new()
        {
            new User
            {
                Id = 1,
                Name = "John"
            },
            new User
            {
                Id = 2,
                Name = "Jack"
            },
            new User
            {
                Id = 3,
                Name = "Mike"
            }
        };
    }
}
