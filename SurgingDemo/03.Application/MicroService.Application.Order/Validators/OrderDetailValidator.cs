using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MicroService.Data.Constant;
using MicroService.Data.Validation;
using MicroService.Entity.Order;
using MicroService.IRespository.Order;
 namespace MicroService.Application.Order.Validators
 {
	 public  class OrderDetailValidator : AbstractValidator<OrderDetail>
	 {
        public OrderDetailValidator(IOrderDetailRespository orderDetailRespository)
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
			
		
										
			RuleFor(m => m.OrderId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"订单id"));
							RuleFor(m => m.OrderId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
											
			RuleFor(m => m.GoodsId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"商品id"));
							RuleFor(m => m.GoodsId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
											
			RuleFor(m => m.Price).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"单价"));
												
			RuleFor(m => m.Count).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"数量"));
								         }
	   }

 }