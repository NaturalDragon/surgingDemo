
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Order.Configuration
 {
	/// <summary>
	/// OrderInfo -映射
	/// </summary>
	public class OrderInfoConfigruation:EntityMappingConfiguration<OrderInfo>
	{
  
         public override void Map(EntityTypeBuilder<OrderInfo> b)
			{ 
			   b.ToTable("OrderInfo").HasKey(p => p.Id);
					   				        			   b.Property(p => p.OrderNumber).IsRequired().HasMaxLength(128); 
			            				   			
	      			   				           
			   b.Property(p => p.TotalMoney).IsRequired(); 
														
	      			   				        			   b.Property(p => p.UserId).IsRequired().HasMaxLength(36); 
			            				   			
	      			   				           
			   b.Property(p => p.ExpireTime).IsRequired(); 
														
	      			   				           
			   b.Property(p => p.Status).IsRequired(); 
														
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