using AdventOfCode.Reader;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Test
{
    public abstract class TestBase
    {
        protected IInputProvider InputProvider
        {
            get
            {
                var inputProviderMock = new Mock<IInputProvider>();
                inputProviderMock.Setup(x => x.GetInput()).Returns(AocInput());
                return inputProviderMock.Object;
            }
        }

        protected abstract string[] AocInput();
    }
}
