
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Org.Configuration
 {
	/// <summary>
	/// Users -映射
	/// </summary>
	public class UsersConfigruation:EntityMappingConfiguration<Users>
	{
  
         public override void Map(EntityTypeBuilder<Users> b)
			{ 
			   b.ToTable("Users").HasKey(p => p.Id);
					   				        			   b.Property(p => p.Name).IsRequired().HasMaxLength(64); 
			            				   			
	      			   				        			   b.Property(p => p.RoleId).IsRequired().HasMaxLength(36); 
			            				   			
	      			   				          
			   b.Property(p => p.PhoneCode).HasMaxLength(16); 
							 			   			
	      			   				        			   b.Property(p => p.Password).IsRequired().HasMaxLength(128); 
			            				   			
	      		        b.Property(p => p.IsDelete).IsRequired(); 
                b.Property(p => p.CreateDate).IsRequired();
				b.Property(p => p.CreateUserId).IsRequired().HasMaxLength(36);
				b.Property(p => p.CreateUserName).IsRequired().HasMaxLength(32);
                b.Property(p => p.ModifyUserId).HasMaxLength(36);;
				b.Property(p => p.ModifyDate);
				b.Property(p => p.ModifyUserName).HasMaxLength(32);
			  }
	}
}