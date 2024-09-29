namespace API.Services;

public interface IApiGuard
{
    void ValidateIds(int expectedId, int actualId);
}
