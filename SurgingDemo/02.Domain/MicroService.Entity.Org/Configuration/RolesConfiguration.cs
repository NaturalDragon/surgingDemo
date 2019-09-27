
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Org.Configuration
 {
	/// <summary>
	/// Roles -映射
	/// </summary>
	public class RolesConfigruation:EntityMappingConfiguration<Roles>
	{
  
         public override void Map(EntityTypeBuilder<Roles> b)
			{ 
			   b.ToTable("Roles").HasKey(p => p.Id);
					   				        			   b.Property(p => p.Name).IsRequired().HasMaxLength(64); 
			            				   			
	      			   				           
			   b.Property(p => p.Level).IsRequired(); 
														
	      			   				        			   b.Property(p => p.Name).IsRequired().HasMaxLength(64); 
			            				   			
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