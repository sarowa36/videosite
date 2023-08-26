using DataAccessLayer.Repositories;
using EntityLayer.Base;
using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Priority;

namespace DataAccessLayer.UnitTest
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public abstract class RepositoryTestBaseClass<T,TRepository> where TRepository:GenericBaseRepository<T>  where T : AOfDefaultContent, new()
    {
        protected ADC db;
        protected TRepository repo;
        public RepositoryTestBaseClass()
        {
            Environment.SetEnvironmentVariable("IsInUnitTest", "1");
            db = new(new Microsoft.EntityFrameworkCore.DbContextOptions<ADC>());
            this.repo = (TRepository)Activator.CreateInstance(typeof(TRepository),db);
        }
        [Fact, Priority(-10)]
        public abstract void TryToAddNewValidValue();
        [Fact]
        public void TryToAddNewInvalidValue()
        {
            var val = new T();
            var result = repo.Create(val);
            Assert.False(string.IsNullOrWhiteSpace(result));
        }
        [Fact, Priority(0)]
        public void CheckValueIsAdded()
        {
            var result = repo.GetAll();
            Assert.Equal(1, result.Count);
        }
        [Fact, Priority(1)]
        public void TryDelete()
        {
            var entity = repo.GetAll().FirstOrDefault();
            var result = repo.Delete(entity);
            Assert.True(string.IsNullOrWhiteSpace(result));
        }
        [Fact, Priority(2)]
        public void DeleteIsWorked()
        {
            var result = repo.GetAll();
            Assert.Equal(0, result.Count);
        }
    }
}
