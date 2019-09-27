using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using MicroService.Data.Constant;
using MicroService.Data.Validation;
using MicroService.Entity.Org;
using MicroService.IRespository.Org;
 namespace MicroService.Application.Org.Validators
 {
	 public  class RolesValidator : AbstractValidator<Roles>
	 {
        public RolesValidator(IRolesRespository rolesRespository)
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
			
		
										
			RuleFor(m => m.Name).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,""));
							RuleFor(m => m.Name).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
											
			RuleFor(m => m.Level).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,""));
												
			RuleFor(m => m.Name).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,""));
							RuleFor(m => m.Name).Length(0,64).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 64));
			         }
	   }

 }