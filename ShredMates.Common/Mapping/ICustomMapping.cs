using AutoMapper;

namespace ShredMates.Common.Mapping
{
    public interface ICustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
