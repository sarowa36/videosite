using CrudProcedures.Procedures.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrudProcedures.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class AllowProceduresAttribute : Attribute
    {
        public AllowProceduresAttribute(params BaseProcedure[] param)
        { 
            Procedures = param;
        }
        private BaseProcedure[] Procedures { get; set; }
    }
}
