using AutoMapper;

namespace ShredMates.Common.Mapping
{
    public interface IMapExplicitly
    {
        public void RegisterMappings(IProfileExpression profile);
    }
}
