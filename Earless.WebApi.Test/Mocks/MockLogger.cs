using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;

namespace Earless.WebApi.Test.Mocks
{
    public class MockLogger <T>
    {
        public ILogger<T> CreateLogger()
        {
            var mock = new Mock<ILogger<T>>();
            ILogger<T> logger = mock.Object;

            return logger;
        }
    }
}
