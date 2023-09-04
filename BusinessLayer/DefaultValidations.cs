using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class DefaultValidations
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MinimumLength(6).MaximumLength(16);
        }
        public static IRuleBuilderOptions<T, int> Id<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.GreaterThan(0).NotEmpty();
        }
        public static IRuleBuilderOptions<T, string> UserName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MinimumLength(8).MaximumLength(16);
        }
        public static IRuleBuilderOptions<T, string> Compare<T>(this IRuleBuilder<T, string> ruleBuilder, Func<T, string> expression)
        {
            return ruleBuilder.Must((model, x) => expression.Invoke(model) == x);
        }
    }
}
