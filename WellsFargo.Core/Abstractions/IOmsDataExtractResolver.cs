namespace WellsFargo.Core.Abstractions
{
    public interface IOmsDataExtractResolver
    {
        IOmsDataExtractStrategy Resolve(string omsType);
    }
}
