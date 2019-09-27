
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Order.Configuration
 {
	/// <summary>
	/// OrderDetail -映射
	/// </summary>
	public class OrderDetailConfigruation:EntityMappingConfiguration<OrderDetail>
	{
  
         public override void Map(EntityTypeBuilder<OrderDetail> b)
			{ 
			   b.ToTable("OrderDetail").HasKey(p => p.Id);
					   				        			   b.Property(p => p.OrderId).IsRequired().HasMaxLength(36); 
			            				   			
	      			   				        			   b.Property(p => p.GoodsId).IsRequired().HasMaxLength(36); 
			            				   			
	      			   				           
			   b.Property(p => p.Price).IsRequired(); 
														
	      			   				           
			   b.Property(p => p.Count).IsRequired(); 
														
	      			   				          			   b.Property(p => p.Money).IsRequired(); 
														
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