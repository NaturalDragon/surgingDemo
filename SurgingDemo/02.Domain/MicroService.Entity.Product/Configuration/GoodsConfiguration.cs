
using MicroService.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

 namespace MicroService.Entity.Product.Configuration
 {
	/// <summary>
	/// Goods -映射
	/// </summary>
	public class GoodsConfigruation:EntityMappingConfiguration<Goods>
	{
  
         public override void Map(EntityTypeBuilder<Goods> b)
			{ 
			   b.ToTable("Goods").HasKey(p => p.Id);
					   				        			   b.Property(p => p.Name).IsRequired().HasMaxLength(128); 
			            				   			
	      			   				           
			   b.Property(p => p.StockNum).IsRequired(); 
														
	      			   				           
			   b.Property(p => p.Price).IsRequired(); 
														
	      			   				        			   b.Property(p => p.CoverImgSrc).IsRequired().HasMaxLength(256); 
			            				   			
	      			   				        			   b.Property(p => p.Details).IsRequired();
							 			   			
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