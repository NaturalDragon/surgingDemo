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
	 public  class UsersValidator : AbstractValidator<Users>
	 {
        public UsersValidator(IUsersRespository usersRespository)
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
											
			RuleFor(m => m.RoleId).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,""));
							RuleFor(m => m.RoleId).Length(0,36).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 36));
										RuleFor(m => m.PhoneCode).Length(0,16).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 16));
											
			RuleFor(m => m.Password).NotNull().WithMessage(x=>string.Format(ErrorMessage.IsRequired,""));
							RuleFor(m => m.Password).Length(0,128).WithMessage(x=>string.Format(ErrorMessage.IsLengthError, 128));
			         }
	   }

 }