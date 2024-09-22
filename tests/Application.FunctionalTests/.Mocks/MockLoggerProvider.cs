using Microsoft.Extensions.Logging;

namespace Application.FunctionalTests.Mocks;

public class MockLoggerProvider : ILoggerProvider
{
    private bool _disposedValue;

    public ILogger CreateLogger(string categoryName)
    {
        return new Mock<ILogger>().Object;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
