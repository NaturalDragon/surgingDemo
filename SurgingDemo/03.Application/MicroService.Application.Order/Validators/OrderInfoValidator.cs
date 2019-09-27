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
	 public  class OrderInfoValidator : AbstractValidator<OrderInfo>
	 {
        public OrderInfoValidator(IOrderInfoRespository orderInfoRespository)
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
			
		
										
			RuleFor(m => m.OrderNumber).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"订单号"));
							RuleFor(m => m.OrderNumber).Length(0,128).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 128));
											
			RuleFor(m => m.TotalMoney).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"总金额"));
												
			RuleFor(m => m.UserId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"下单用户"));
							RuleFor(m => m.UserId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
											
			RuleFor(m => m.ExpireTime).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"过期时间"));
												
			RuleFor(m => m.Status).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,"订单状态"));
				         }
	   }

 }