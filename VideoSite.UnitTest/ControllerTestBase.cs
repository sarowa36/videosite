using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Priority;

namespace VideoSite.UnitTest
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)
        , DefaultPriority(0)]
    public abstract class ControllerTestBase
    {
        public ControllerTestBase()
        {
            Environment.SetEnvironmentVariable("IsInUnitTest", "1");
        }
    }
}
