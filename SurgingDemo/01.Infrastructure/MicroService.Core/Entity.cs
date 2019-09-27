using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicroService.Core
{
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {
        string IEntity<string>.Id { get; set; }
    }

    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {

        protected Entity()
        {
            IsDelete = false;
            CreateDate = DateTime.Now;
        }
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        [Key]
        [StringLength(36)]
        public virtual TPrimaryKey Id { get; set; }

        [Column("IsDelete", TypeName = "bit")]
        [DefaultValue(false)]
        [Required]
        public virtual bool IsDelete { set; get; }
        [Required]
        public virtual DateTime CreateDate { set; get; }

        [Required]
        [StringLength(36)]
        public string CreateUserId { set; get; }

        [Required]
        [StringLength(32)]
        public string CreateUserName { set; get; }

        [StringLength(36)]
        public string ModifyUserId { set; get; }

        public Nullable<DateTime> ModifyDate { set; get; }

        [StringLength(32)]
        public string ModifyUserName { set; get; }

        //[ConcurrencyCheck()]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[Column(TypeName = "timestamp")]
        //public virtual DateTime Timestamp { set; get; }
        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }
    }
}
