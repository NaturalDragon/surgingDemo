using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MicroService.Data.Constant;
using MicroService.Data.Validation;
using MicroService.Entity.Product;
using MicroService.IRespository.Product;
 namespace MicroService.Application.Product.Validators
 {
	 public  class GoodsValidator : AbstractValidator<Goods>
	 {
        public GoodsValidator(IGoodsRespository goodsRespository)
        {
            RuleSet(ValidatorTypeConstants.Create, () =>
            {
                BaseValidator();
            });
            RuleSet(ValidatorTypeConstants.Modify, () =>
            {
                BaseValidator();
            });

        }

        void BaseValidator()
	     {
		    RuleFor(per => per.Id).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "主键Id"));
            RuleFor(per => per.CreateDate).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "创建时间CreateDate"));
            RuleFor(per => per.IsDelete).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "删除状态IsDelete"));
		   
		    RuleFor(per => per.CreateUserId).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "创建人CreateUserId"));
            RuleFor(per => per.CreateUserName).NotNull().WithMessage(x => string.Format(ErrorMessage.IsRequired, "创建人姓名CreateUserName"));
			
		
										
			RuleFor(m => m.Name).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"商品名称"));
							RuleFor(m => m.Name).Length(0,128).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 128));
											
			RuleFor(m => m.StockNum).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"库存"));
												
			RuleFor(m => m.Price).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"单价"));
												
			RuleFor(m => m.CoverImgSrc).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"封面图"));
							RuleFor(m => m.CoverImgSrc).Length(0,256).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 256));
											
			RuleFor(m => m.Details).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"详情"));
				         }
	   }

 }