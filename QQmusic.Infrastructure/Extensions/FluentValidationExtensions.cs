using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace QQmusic.Infrastructure.Extensions
{
    public static class FluentValidationExtensions
    {
        public static void AddFluentValidators(this IServiceCollection services)
        {
            //校验资源
            //services.AddTransient<IValidator<UserLoginResource>, UserLoginResourceValidator<UserLoginResource>>();
            //services.AddTransient<IValidator<PersonAddResource>, PersonAddResourceValidator<PersonAddResource>>();
            //services.AddTransient<IValidator<BusinessBoardAddResource>, BusinessBoardAddResourceValidator<BusinessBoardAddResource>>();
            //services.AddTransient<IValidator<ContractBoardAddResource>, ContractBoardAddResourceValidator<ContractBoardAddResource>>();
            //services.AddTransient<IValidator<List<ContractBoardAddResource>>, ContractBoardAddResourceListValidator>();
            //services.AddTransient<IValidator<OrderDetailAddOrUpdateResource>, OrderDetailAddOrUpdateResourceValidator<OrderDetailAddOrUpdateResource>>();
            //services.AddTransient<IValidator<OrderDetailAddResource>, OrderDetailAddResourceValidator>();
            //services.AddTransient<IValidator<OrderDetailUpdateResource>, OrderDetailUpdateResourceValidator>();
            //services.AddTransient<IValidator<OrderMeasureAddOrUpdateResource>, OrderMeasureAddOrUpdateResourceValidator<OrderMeasureAddOrUpdateResource>>();
            //services.AddTransient<IValidator<BusinessGuimaAddResource>, BusinessGuimaAddResourceValidator<BusinessGuimaAddResource>>();
            //services.AddTransient<IValidator<BodyAddResource>, BodyAddResourceValidator<BodyAddResource>>();
            //services.AddTransient<IValidator<ContractAddResource>, ContractAddResourceValidator<ContractAddResource>>();

            ////services.AddTransient<IValidator<BackupDetailAddResource>, BackupDetailAddResourceValidator<BackupDetailAddResource>>();
            //services.AddTransient<IValidator<List<BackupDetailAddResource>>, BackupDetailAddResourceListValidator>();
            ////services.AddTransient<IValidator<OrderAddResource>, OrderAddResourceValidator<OrderAddResource>>();
            //services.AddTransient<IValidator<OrderUpdateResource>, OrderUpdateResourceValidator<OrderUpdateResource>>();

            //services.AddTransient<IValidator<MeasureItemUpdateResource>, MeasureItemUpdateResourceValidator>();
            //services.AddTransient<IValidator<MeasureItemAddResource>, MeasureItemAddResourceValidator>();
        }
    }
}
