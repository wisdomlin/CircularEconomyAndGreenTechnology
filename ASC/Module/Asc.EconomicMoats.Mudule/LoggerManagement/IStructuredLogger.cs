namespace Asc
{
    public interface IStructuredLogger
    {
        void Trace(string v);
        void Debug(string v);
        void Info(string v);
        void Warn(string v);
        void Error(string v);
        void Fatal(string v);
    }
}
