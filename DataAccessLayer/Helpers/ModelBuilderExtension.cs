using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

public static class ModelBuilderExtensions
{
    public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder, Expression<Func<TInterface, bool>> expression)
    {
        var entities = modelBuilder.Model
            .GetEntityTypes()
            .Where(t => t.BaseType == null)
            .Select(t => t.ClrType)
            .Where(t => typeof(TInterface).IsAssignableFrom(t));
        foreach (var entity in entities)
        {
            var newParam = Expression.Parameter(entity);
            var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
            modelBuilder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
        }
    }
    public static void ApplyGlobalInclude<TInterface, TProperty>(this ModelBuilder modelBuilder, Expression<Func<TInterface, TProperty>> expression) where TProperty : class
    {
        var entities = modelBuilder.Model
            .GetEntityTypes()
            .Where(t => t.BaseType == null)
            .Select(t => t.ClrType)
            .Where(t => typeof(TInterface).IsAssignableFrom(t));
        foreach (var entity in entities)
        {
            modelBuilder.Entity(entity).Navigation(expression.ReturnType.Name).AutoInclude();
        }
    }
}

