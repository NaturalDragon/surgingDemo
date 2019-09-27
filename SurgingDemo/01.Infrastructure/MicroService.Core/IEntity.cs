using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Core
{

    public interface IEntity : IEntity<string>
    {
      
    }

    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { set; get; }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an <see cref="Id"/>).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();

        bool IsDelete { set; get; }

        DateTime CreateDate { set; get; }


        string CreateUserId { set; get; }

        string CreateUserName { set; get; }


        string ModifyUserId { set; get; }


        Nullable<DateTime> ModifyDate { set; get; }

        string ModifyUserName { set; get; }

        // DateTime Timestamp { set; get; }
    }
}
