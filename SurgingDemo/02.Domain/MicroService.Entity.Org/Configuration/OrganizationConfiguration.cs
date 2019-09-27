
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Org.Configuration
 {
	/// <summary>
	/// Organization -映射
	/// </summary>
	public class OrganizationConfigruation:EntityMappingConfiguration<Organization>
	{
  
         public override void Map(EntityTypeBuilder<Organization> b)
			{ 
			   b.ToTable("Organization").HasKey(p => p.Id);
					   				        			   b.Property(p => p.Name).IsRequired().HasMaxLength(64); 
			            				   			
	      			   				        			   b.Property(p => p.ParentId).IsRequired().HasMaxLength(36); 
			            				   			
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